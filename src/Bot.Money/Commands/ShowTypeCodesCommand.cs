﻿using Bot.Core.Exceptions;
using Bot.Money.Enums;
using Bot.Money.Interfaces;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Money.Commands
{
    public class ShowTypeCodesCommand : IMoneyCommand
    {
        private const string EXPENSE = "/exp";
        private const string INCOME = "/inc";
        public bool CanExecute(Message message)
        {
            return message.Text == EXPENSE || message.Text == INCOME;
        }

        public async Task Execute(Message message, ITelegramBotClient botClient)
        {
            if (message.Text == EXPENSE)
            {
                var types = $"Expense types codes: {PrintTypeCodes(Enum.GetNames(typeof(ExpenseCategory)))}";
                await botClient.SendTextMessageAsync(message.Chat, types, ParseMode.Default, false, false, 0);
            }
            else if (message.Text == INCOME)
            {
                var types = $"Income types codes: {PrintTypeCodes(Enum.GetNames(typeof(IncomeCategory)))}";
                await botClient.SendTextMessageAsync(message.Chat, types, ParseMode.Default, false, false, 0);
            }
            else
            {
                throw new NotFoundCommandException();
            }
        }

        private string PrintTypeCodes(string[] input)
        {
            var strBuiler = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                strBuiler.Append($"\n{i + 1}. {input[i]}");
            }
            return strBuiler.ToString();
        }
    }
}