# GAA dotnet templates

DotNet шаблоны для личных проектов.

## [Примеры по созданию шаблонов](https://github.com/dotnet/dotnet-template-samples)

## Команды по работе с пакетом шаблонов

- Просмотр актуального перечня шаблонов
  ```console
  dotnet new list
  ```

- Установка пакета с шаблонами
  ```console
  dotnet new install Gaa.DotNet.Templates.1.0.0.nupkg
  ```

- Удаление пакета с шаблонами
  ```console
  dotnet new uninstall Gaa.DotNet.Templates.1.0.0.nupkg
  ```

## Список доступных шаблонов

```console
Templates               Short Name                              Language    Tags
------------------------------------------------------------------------------------------------------
Gaa: Empty Solution     gaa-sln,gaa-solution                    [C#]        Common/Gaa/Solution
Gaa: Project Test       gaa-proj-test,gaa-project-test          [C#]        Common/Gaa/Project/Test
Gaa: Project Service    gaa-proj-service,gaa-project-service    [C#]        Common/Gaa/Project/Service
Gaa: Project Logic      gaa-proj-logic,gaa-project-logic        [C#]        Common/Gaa/Project/Logic
Gaa: Project Library    gaa-proj-library,gaa-project-library    [C#]        Common/Gaa/Project/Library
Gaa: Project Console    gaa-proj-console,gaa-project-console    [C#]        Common/Gaa/Project/Console
```