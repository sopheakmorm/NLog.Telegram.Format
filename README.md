NLog.Telegram.Format
==========

An NLog target for Telegram with the avaiable format which provided by Telegram.

[![Version](https://badge.fury.io/nu/NLog.Telegram.Format.svg)](https://www.nuget.org/packages/NLog.Telegram.Format)

Code forked from repository : https://github.com/narfunikita/NLog.Telegram

------------

Usage
=====
1. Create a TelegramBot(https://core.telegram.org/bots#3-how-do-i-create-a-bot).
2. Configure NLog to use `NLog.Telegram.Format`: https://github.com/nlog/nlog/wiki/Configuration-file

### Example NLog.config

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

  <extensions>
    <add assembly="NLog.Telegram.Format" />
  </extensions>

  <targets async="true">
    <target xsi:type="Telegram"
            name="telegramTarget"
            layout="${message}"
            botToken="xxx"
            chatId="xxx"
            />
  </targets>

  <rules>
    <logger name="*" minlevel="Trace" writeTo="telegramTarget" />
  </rules>
</nlog>
```
---------
### Example Formatting
```csharp
	var log = LogManager.GetCurrentClassLogger();

	log.Debug($"`hello world` **hello world** __hello world__");
```

For more: https://core.telegram.org/bots/api#formatting-options

### Configuration Options

Key        | Description
----------:| -----------
BotToken   | Your telegram bot token (e.g 123456:ABC-DEF1234ghIkl-zyx57W2v1u123ew11)
ChatId     | Unique identifier for the message recipient � User or GroupChat id
BaseUrl    | Optional. Api bot Url. Default: https://api.telegram.org/bot

----------
Reference: https://github.com/narfunikita/NLog.Telegram
