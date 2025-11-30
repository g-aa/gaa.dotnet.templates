# GAA dotnet templates

DotNet шаблоны для личных проектов.

## [Примеры по созданию шаблонов](https://github.com/dotnet/dotnet-template-samples)

## Список доступных шаблонов

- Просмотр актуального перечня шаблонов
  ```console
  dotnet new list

  Templates               Short Name           Language    Tags
  ----------------------------------------------------------------------------
  Gaa: Empty Solution     gaa.solution           [C#]      Gaa/Solution
  Gaa: Project Test       gaa.project.test       [C#]      Gaa/Project/Test
  Gaa: Project Service    gaa.project.service    [C#]      Gaa/Project/Service
  Gaa: Project Logic      gaa.project.logic      [C#]      Gaa/Project/Logic
  Gaa: Project Library    gaa.project.library    [C#]      Gaa/Project/Library
  Gaa: Project Console    gaa.project.console    [C#]      Gaa/Project/Console
  ```

## Команды по работе с пакетом шаблонов

- Установка пакета с шаблонами
  ```console
  dotnet new install Gaa.DotNet.Templates.1.0.0.nupkg
  ```

- Просмотреть перечень установленных пакетов с шаблонами
  ```console
  dotnet new uninstall

  Установленные в настоящее время элементы:
   Gaa.DotNet.Templates
      Версия: 1.0.0
      Сведения:
         Author: Andrey G.
         Reserved: ✘
      Шаблоны:
         Gaa: Empty Solution (gaa.solution) C#
         Gaa: Project Console (gaa.project.console) C#
         Gaa: Project Library (gaa.project.library) C#
         Gaa: Project Test (gaa.project.test) C#
      Команда "Удалить":
         dotnet new uninstall Gaa.DotNet.Templates
  ```

- Удаление пакета с шаблонами
  ```console
  dotnet new uninstall Gaa.DotNet.Templates
  ```

## Принятая структура проекта

### Наименование решений проектов

- Наименование решения должно иметь вид: `{наименования организации}.{наименование продукта}.{компонент}.sln`.\
  Пример: `gaa.extensions.sln`.

- Наименование C# проекта должно иметь вид: `{наименования организации}.{наименование продукта}.{компонент}.{наименование C# проекта}.csproj`.\
  Пример: `Gaa.Extensions.Core.csproj`.

### Решение

- Перечень основных каталогов и файлов решения
  ```
  gaa.extensions/ - каталог проекта
  │
  ├── build/
  │   ├── coverlet.runsettings - файл конфигурации coverlet.collector
  │   ├── Version.targets      - общие параметры всех проектов решения для MSBuild
  │   └── ...                  - другие полезные файлы для сборки, тестирования и развертывания проекта
  │
  ├── misc/
  |   ├── gaa.extensions.sln   - файл решения
  │   ├── .editorconfig        - параметры для EditorConfig, предпостетельно приметять с Gaa.DotNet.CodeAnalysis
  │   ├── .gitignore
  │   ├── .version             - версия проекта или релиза должна соответствоать томеру тэга
  │   ├── CHANGELOG.md         - история изменения
  │   ├── README.md            - описание проекта
  │   ├── TODO                 - перечень задач на исполнение
  │   └── ...                  - прочие файлы
  |
  ├── src/
  │   ├── Gaa.Extensions.Core/                - C# проект
  │   │   ├── Gaa.Extensions.Core.csproj
  │   │   └── ...
  │   └── ...                                 - следующий C# проект
  │
  ├── test/
  |   ├── Gaa.Extensions.Core.Test/           - C# проект с набором тестов
  │   │   ├── Gaa.Extensions.Core.Test.csproj
  │   │   └── ...
  │   └── ...                                 - следующий C# проект с набором тестов
  │
  └── ...
  ```

### Тесты

- На каждый C# предпочтительно создавать отдельный проект с тестами.\
  Наименование проекта с тестами должно иметь постфикс `Test`.

- Наименование проекта с тестами должно соответствовать наименованию C# проекта.\
  Пример: `Gaa.Extensions.Core.csproj` -> `Gaa.Extensions.Core.Test.csproj`.