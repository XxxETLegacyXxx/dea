﻿using DEA.Services;
using DEA.SQLite.Repository;
using System;
using System.Threading.Tasks;

namespace Discord.Commands
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RequireCooldownAttribute : PreconditionAttribute
    {
        public override async Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IDependencyMap map)
        {
            TimeSpan cooldown = Config.DEFAULT_COOLDOWN;
            DateTime lastUse = .UtcNow;
            var user = UserRepository.FetchUser(context as SocketCommandContext);
            switch (command.Name.ToLower())
            {
                case "whore":
                    cooldown = Config.WHORE_COOLDOWN;
                    lastUse = user.Cooldowns.Whore;
                    break;
                case "jump":
                    cooldown = Config.JUMP_COOLDOWN;
                    lastUse = user.Cooldowns.Jump;
                    break;
                case "steal":
                    cooldown = Config.STEAL_COOLDOWN;
                    lastUse = user.Cooldowns.Steal;
                    break;
                case "rob":
                    cooldown = Config.ROB_COOLDOWN;
                    lastUse = user.Cooldowns.Rob;
                    break;
                case "withdraw":
                    cooldown = Config.WITHDRAW_COOLDOWN;
                    lastUse = user.Cooldowns.Withdraw;
                    break;
            }
            if (.UtcNow.Subtract(lastUse).TotalMilliseconds > cooldown.TotalMilliseconds)
                return PreconditionResult.FromSuccess();
            else
            {
                await Logger.Cooldown(context as SocketCommandContext, command.Name, cooldown.Subtract(.UtcNow.Subtract(lastUse)));
                return PreconditionResult.FromError("");
            }
        }
    }
}