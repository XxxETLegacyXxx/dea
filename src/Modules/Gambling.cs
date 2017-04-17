﻿using Discord.Commands;
using System.Threading.Tasks;
using DEA.Common.Preconditions;
using DEA.Common;
using DEA.Services;

namespace DEA.Modules
{
    public class Gambling : DEAModule
    {
        private GamblingService _gamblingService;

        public Gambling(GamblingService gamblingService)
        {
            _gamblingService = gamblingService;
        }

        [Command("21+")]
        [Summary("Roll 20.84 or higher on a 100.00 sided die, win 0.2X your bet.")]
        public Task XHalf(decimal bet)
            => _gamblingService.GambleAsync(Context, bet, 20.84m, 0.2m);

        [Command("50x2")]
        [Require(Attributes.FiftyX2)]
        [Summary("Roll 50.00 or higher on a 100.00 sided die, win your bet.")]
        public Task X2BetterOdds(decimal bet)
            => _gamblingService.GambleAsync(Context, bet, 50.0m, 1.0m);

        [Command("53x2")]
        [Summary("Roll 52.50 or higher on a 100.00 sided die, win your bet.")]
        public Task X2(decimal bet)
            => _gamblingService.GambleAsync(Context, bet, 52.5m, 1.0m);

        [Command("75+")]
        [Summary("Roll 75.01 or higher on a 100.00 sided die, win 2.8X your bet.")]
        public Task X3dot8(decimal bet)
            => _gamblingService.GambleAsync(Context, bet, 75.01m, 2.8m);

        [Command("100x9500")]
        [Summary("Roll 100.00 on a 100.00 sided die, win 9500X your bet.")]
        public Task X90(decimal bet) 
            => _gamblingService.GambleAsync(Context, bet, 100.0m, 9500.0m);

    }
}
