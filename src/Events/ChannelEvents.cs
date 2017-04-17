﻿using DEA.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DEA.Events
{
    class ChannelEvents
    {
        private IDependencyMap _map;
        private DiscordSocketClient _client;
        private LoggingService _loggingService;

        public ChannelEvents(IDependencyMap map)
        {
            _map = map;
            _client = _map.Get<DiscordSocketClient>();
            _loggingService = _map.Get<LoggingService>();
            _client.ChannelCreated += HandleChannelCreated;
            _client.ChannelDestroyed += HandleChannelDestroyed;
            _client.ChannelUpdated += HandleChannelUpdated;
        }

        private async Task HandleChannelCreated(SocketChannel socketChannel)
        {
            await DetailedLogsForChannel(socketChannel, "Channel Creation", new Color(12, 255, 129)); 
        }

        private async Task HandleChannelUpdated(SocketChannel socketChannelBefore, SocketChannel socketChannelAfter)
        {
            await DetailedLogsForChannel(socketChannelAfter, "Channel Modification", new Color(12, 255, 129));
        }

        private async Task HandleChannelDestroyed(SocketChannel socketChannel)
        {
            await DetailedLogsForChannel(socketChannel, "Channel Deletion", new Color(255, 0, 0));
        }

        private async Task DetailedLogsForChannel(SocketChannel socketChannel, string action, Color color)
        {
            if (!(socketChannel is SocketTextChannel) && !(socketChannel is SocketVoiceChannel)) return;
            string channelType;
            string channelName;
            SocketGuild guild;
            if (socketChannel is SocketTextChannel)
            {
                channelType = "Text Channel";
                channelName = (socketChannel as SocketTextChannel).Name;
                guild = (socketChannel as SocketTextChannel).Guild;
            }
            else
            {
                channelType = "Voice Channel";
                channelName = (socketChannel as SocketVoiceChannel).Name;
                guild = (socketChannel as SocketVoiceChannel).Guild;
            }
            SocketChannel channel = socketChannel as SocketTextChannel;
            await _loggingService.DetailedLogAsync(guild, "Action", action, channelType, channelName, socketChannel.Id, color);
        }
    }
}
