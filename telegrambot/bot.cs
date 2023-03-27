using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

var botClient = new TelegramBotClient("");
using var cts = new CancellationTokenSource();


botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: new ReceiverOptions
    {
        AllowedUpdates = { }
    },
    cancellationToken: cts.Token
);
cts.Cancel();
while (true)
{
    var request = Console.ReadLine();
    botClient.SendTextMessageAsync(chatId: 1,text: request);
}


async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    var message = update.Message.Text;
    var user = update.Message.Chat.Id;
    Console.WriteLine($"Received a '{message}' message in chat {user}.");
    Message massage = await botClient.SendTextMessageAsync(
        chatId: user,
        text: "asd",
        cancellationToken: cancellationToken);
}
Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}