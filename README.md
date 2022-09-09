# Jira Task Creator

[![.NET](https://github.com/letyshub/jira-task-creator/actions/workflows/dotnet.yml/badge.svg)](https://github.com/letyshub/jira-task-creator/actions/workflows/dotnet.yml)

Jira Task Creator read links from the given file and creates Jira's task. I use it to create Read Later's task. So, when I find important article I create Jira's task to read it later. Jira Task Creator deletes all links from file when it finishes work.

## Settings

All settings are in `appsettings.json` file.

```json
{
    "Settings": {
        "Timeout": 2,
        "Jira": {
            "Project": "",
            "TaskParentId": "",
            "User": "",
            "Token": "",
            "Url": ""
        },
        "LinksFilePath": ""
    }
}
```

| Setting              | Description                                                                                                             |
| -------------------- | ----------------------------------------------------------------------------------------------------------------------- |
| Timeout              | Running timeout (in minutes)                                                                                            |
| Jira -> Project      | Name of Jira's project                                                                                                  |
| Jira -> TaskParentId | Id of epic                                                                                                              |
| Jira -> User         | Your Jira's username                                                                                                    |
| Jira -> Token        | Your Jira's [token](https://support.atlassian.com/atlassian-account/docs/manage-api-tokens-for-your-atlassian-account/) |
| Jira -> Url          | Url to your Jira's project                                                                                              |
| LinksFilePath        | Path to file which contains links to create Jira's tasks                                                                |

## Troubleshooting

Application logs messages to `./logs/log.txt` files.
