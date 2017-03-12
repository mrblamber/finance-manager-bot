﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinanceManager.Bot.Framework.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FinanceManager.Bot.Server.Controllers
{
    [Route("/webhook")]
    public class FinanceManagerBotController : Controller
    {
        private readonly ITelegramBotClient _botClient;

        private readonly CommandService _commandService;

        public FinanceManagerBotController(
            ITelegramBotClient botClient, 
            CommandService commandService)
        {
            _botClient = botClient;
            _commandService = commandService;
        }

        [HttpPost("")]
        public IActionResult GetMessage([FromBody]Update update)
        {
            var message = update.Message;

            if (message.Type != MessageType.TextMessage)
            {
                // TODO: add to unhandle
            }

            _commandService.ExecuteCommand(message.Text.Split(' ')[0], message);

            return Ok();
        }
    }
}