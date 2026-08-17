# ECS: первый запуск

Установка **Entitas 1.14.1 + Jenny** в новый Unity-проект на **Windows**, с запуском генерации из **Visual Studio** по горячей клавише.

Проверено на Unity 6000.3.14f1, Windows 10, .NET SDK 6/8/9/10.

---

## Коротко: почему инструкция такая

Дистрибутив Entitas 1.14.1 собран на macOS и рассчитан на генерацию **вне** Unity:

- в архиве нет `Jenny.exe` — только unix-скрипты и `.bat`, зовущие несуществующий exe;
- `Jenny.Generator.Cli.dll` собран под `net6.0`, а Unity работает на Mono — загрузить эти сборки в свой процесс она не может;
- плагины кодогенерации Entitas есть **только** в net6-варианте, Unity-совместимых сборок в комплекте нет.

Поэтому кнопка `Generate` в окне Jenny в этой версии не работает, и генерация запускается внешним процессом. Ниже — как сделать это одной горячей клавишей из Visual Studio.

---

## Предварительно

Нужен **.NET 6 runtime**. Проверить:

```powershell
dotnet --list-runtimes
```

В выводе должна быть строка `Microsoft.NETCore.App 6.x`. Если нет — поставить с [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/6.0).

---

## Шаг 1. Company Name и Product Name

**Сделать сразу после создания проекта, до открытия окна Jenny.**

`Edit → Project Settings → Player` → задать свои значения вместо дефолтных от шаблона (`Unity-Technologies` / `com.unity.template.urp-blank`).

Unity хранит путь к `Jenny.properties` в EditorPrefs — в реестре, с ключом, собранным из этих двух имён:

```
HKEY_CURRENT_USER\Software\Unity Technologies\Unity Editor 5.x
  <company>-<product>-Jenny.Generator.Unity.Editor.PropertiesPath_h<hash>
```

Ключ общий для всех проектов с одинаковыми именами. Оставите дефолтные — новый проект подхватит путь от предыдущего и выдаст:

> Could not find a part of the path "...\Jenny.properties"

### Если это уже случилось

Закрыть Unity (иначе она перезапишет реестр при выходе), сделать бэкап и удалить параметр:

```powershell
reg export "HKCU\Software\Unity Technologies\Unity Editor 5.x" "$env:USERPROFILE\Desktop\unity-editorprefs-backup.reg"

Get-Item 'HKCU:\Software\Unity Technologies\Unity Editor 5.x' |
  Select-Object -ExpandProperty Property |
  Where-Object { $_ -match 'PropertiesPath' }
```

Найденное имя подставить в:

```powershell
Remove-ItemProperty -Path 'HKCU:\Software\Unity Technologies\Unity Editor 5.x' -Name '<имя_параметра>'
```

---

## Шаг 2. Разложить файлы

Из распакованного архива Entitas 1.14.1:

| Из архива | Куда |
|---|---|
| `Entitas/` | `<Проект>/Assets/Entitas/` |
| `Jenny/Jenny/` | `<Проект>/Jenny/` (в корень, рядом с `Assets`) |

**Не копировать** `Jenny-Auto-Import`, `Jenny-Server` (unix-скрипты без расширения) и одноимённые `.bat` из архива — они зовут `Jenny.exe`, которого в дистрибутиве нет.

Итоговая структура:

```
<Проект>/
├── Assets/
│   └── Entitas/
│       ├── DesperateDevs/
│       ├── Entitas/          ← Entitas.dll, Entitas.CodeGeneration.Attributes.dll
│       ├── Jenny/
│       ├── Sherlog/
│       └── TCPeasy/
├── Jenny/
│   ├── Plugins/
│   │   ├── Entitas/          ← плагины кодогенерации (net6)
│   │   └── Jenny/            ← Roslyn и прочее (net6)
│   └── Jenny.Generator.Cli.dll
├── Jenny.properties
└── Jenny-Generate.bat
```

Почистить мусор macOS:

```powershell
Get-ChildItem . -Recurse -Force -Filter '.DS_Store' | Remove-Item -Force
```

---

## Шаг 3. `Jenny.properties`

Создать в **корне проекта** (рядом с `Assets`) со следующим содержимым:

```properties
Jenny.SearchPaths = Jenny\Plugins\Entitas, \
                    Jenny\Plugins\Jenny, \
                    Assets\Entitas\Entitas, \
                    Assets\Entitas\DesperateDevs
Jenny.Plugins = Entitas.CodeGeneration.Plugins, \
                Entitas.Roslyn.CodeGeneration.Plugins, \
                Entitas.VisualDebugging.CodeGeneration.Plugins, \
                Jenny.Plugins, \
                Jenny.Plugins.Unity
Jenny.PreProcessors = Jenny.Plugins.ValidateProjectPathPreProcessor
Jenny.DataProviders = Entitas.CodeGeneration.Plugins.ContextDataProvider, \
                      Entitas.Roslyn.CodeGeneration.Plugins.CleanupDataProvider, \
                      Entitas.Roslyn.CodeGeneration.Plugins.ComponentDataProvider, \
                      Entitas.Roslyn.CodeGeneration.Plugins.EntityIndexDataProvider
Jenny.CodeGenerators = Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator, \
                       Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator, \
                       Entitas.CodeGeneration.Plugins.ComponentEntityApiInterfaceGenerator, \
                       Entitas.CodeGeneration.Plugins.ComponentGenerator, \
                       Entitas.CodeGeneration.Plugins.ComponentLookupGenerator, \
                       Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator, \
                       Entitas.CodeGeneration.Plugins.ContextAttributeGenerator, \
                       Entitas.CodeGeneration.Plugins.ContextGenerator, \
                       Entitas.CodeGeneration.Plugins.ContextMatcherGenerator, \
                       Entitas.CodeGeneration.Plugins.ContextsGenerator, \
                       Entitas.CodeGeneration.Plugins.EntityGenerator, \
                       Entitas.CodeGeneration.Plugins.EntityIndexGenerator, \
                       Entitas.CodeGeneration.Plugins.EventEntityApiGenerator, \
                       Entitas.CodeGeneration.Plugins.EventListenerComponentGenerator, \
                       Entitas.CodeGeneration.Plugins.EventListenerInterfaceGenerator, \
                       Entitas.CodeGeneration.Plugins.EventSystemGenerator, \
                       Entitas.CodeGeneration.Plugins.EventSystemsGenerator, \
                       Entitas.Roslyn.CodeGeneration.Plugins.CleanupSystemGenerator, \
                       Entitas.Roslyn.CodeGeneration.Plugins.CleanupSystemsGenerator, \
                       Entitas.VisualDebugging.CodeGeneration.Plugins.ContextObserverGenerator, \
                       Entitas.VisualDebugging.CodeGeneration.Plugins.FeatureClassGenerator
Jenny.PostProcessors = Jenny.Plugins.AddFileHeaderPostProcessor, \
                       Jenny.Plugins.CleanTargetDirectoryPostProcessor, \
                       Jenny.Plugins.MergeFilesPostProcessor, \
                       Jenny.Plugins.NewLinePostProcessor, \
                       Jenny.Plugins.UpdateCsprojPostProcessor, \
                       Jenny.Plugins.WriteToDiskPostProcessor, \
                       Jenny.Plugins.ConsoleWriteLinePostProcessor
Jenny.Server.Port = 3333
Jenny.Client.Host = localhost
Jenny.Plugins.ProjectPath = Assembly-CSharp.csproj
Jenny.Plugins.TargetDirectory = Assets
Entitas.CodeGeneration.Plugins.Assemblies =
Entitas.CodeGeneration.Plugins.Contexts = Game, \
                                          Input
Entitas.CodeGeneration.Plugins.IgnoreNamespaces = true
```

Файл переносится между проектами как есть — все пути относительные. **Под проект меняется только `Contexts`** — список ваших контекстов.

### Что здесь принципиально и почему

| Строка | Зачем |
|---|---|
| `Assets\Entitas\Entitas` и `Assets\Entitas\DesperateDevs` в `SearchPaths` | Плагинам нужны `Entitas.dll` и `Entitas.CodeGeneration.Attributes.dll`. В `Jenny\Plugins\` их нет — только здесь. Без этих путей: `Could not load file or assembly 'Entitas, Version=1.0.0.0'` |
| Только Roslyn-провайдеры + `ContextDataProvider` | Рефлексионные `Entitas.CodeGeneration.Plugins.ComponentDataProvider` / `.EntityIndexDataProvider` грузят скомпилированную `Assembly-CSharp.dll` и падают на несовместимости рантаймов. Roslyn читает исходники через `.csproj` |
| Нет `Jenny.Plugins.Unity.WarnIfCompilationErrorsPreProcessor` | Использует Unity API, вне Unity не работает |
| Нет `Jenny.Plugins.Unity.DebugLogPostProcessor` | То же самое |
| `Entitas.CodeGeneration.Plugins.Assemblies` пустой | Нужен только рефлексионным провайдерам, которых мы не используем |
| Нет `Assets\Entitas\Jenny\Editor\Jenny` в `SearchPaths` | Там лежат дубликаты `Jenny.Plugins.dll` под Unity. Попадут в SearchPaths — генератор загрузит их вместо net6-версий и упадёт |
| `IgnoreNamespaces = true` | Короткие имена в API: `entity.worldPosition`, а не `entity.commonWorldPosition` |
| `TargetDirectory = Assets` | Код генерируется в `Assets/Generated` |

---

## Шаг 4. `Jenny-Generate.bat`

В корень проекта:

```bat
@echo off
pushd %~dp0
dotnet Jenny\Jenny.Generator.Cli.dll gen
popd
pause
```

Работает двойным кликом. `pause` оставлен, чтобы окно не закрывалось и был виден лог.

Ещё два полезных, по желанию:

**`Jenny-Doctor.bat`** — диагностика конфига:

```bat
@echo off
pushd %~dp0
dotnet Jenny\Jenny.Generator.Cli.dll doctor
popd
pause
```

**`Jenny-Server.bat`** — если захотите кнопку в Unity (см. приложение):

```bat
@echo off
pushd %~dp0
echo Jenny server on port 3333. Keep this window open.
dotnet Jenny\Jenny.Generator.Cli.dll server
popd
pause
```

---

## Шаг 5. Автозапуск из Visual Studio

Цель: нажал горячую клавишу — код сгенерирован, лог в окне Output, никаких всплывающих консолей.

### 5.1. Завести внешний инструмент

`Tools → External Tools… → Add`

| Поле | Значение |
|---|---|
| **Title** | `Jenny Generate` |
| **Command** | `C:\Program Files\dotnet\dotnet.exe` |
| **Arguments** | `Jenny\Jenny.Generator.Cli.dll gen` |
| **Initial directory** | `$(SolutionDir)` |
| **Use Output window** | ☑ включить |
| **Close on exit** | ☑ включить |
| **Prompt for arguments** | ☐ выключить |

`OK`.

> Если `dotnet.exe` лежит в другом месте — путь покажет `(Get-Command dotnet).Source`.

> `$(SolutionDir)` работает, когда `.sln` / `.slnx` лежит в корне проекта Unity. Так Unity его и генерирует. Если решение перенесено — впишите абсолютный путь к корню проекта.

### 5.2. Запомнить позицию в списке

В окне `External Tools` инструменты идут списком. Посчитайте, **каким по счёту** оказался `Jenny Generate` — первым, вторым и так далее. Этот номер понадобится дальше.

### 5.3. Назначить горячую клавишу

`Tools → Options → Environment → Keyboard`

1. В поле **Show commands containing** ввести `Tools.ExternalCommand`
2. Выбрать `Tools.ExternalCommand<N>`, где `N` — номер из шага 5.2
3. Поставить курсор в **Press shortcut keys** и нажать сочетание — например `Ctrl+Shift+J`
4. Проверить строку **Shortcut currently used by**: если там что-то есть, сочетание занято, возьмите другое
5. `Assign` → `OK`

### 5.4. Проверить

Нажать сочетание. В `View → Output`, канал **External Tools**, должно появиться:

```
Generating using Jenny.properties
...
[??:??:??] Generated N files in X seconds
```

> **Важно про нумерацию.** `Tools.ExternalCommand<N>` привязан к позиции в списке, а не к названию. Добавите позже другой инструмент выше по списку — горячая клавиша начнёт запускать его. В этом случае перепривяжите шорткат на новый номер.

---

## Рабочий цикл

1. Пишете компонент:

   ```csharp
   using Entitas;
   using UnityEngine;

   [Game] public class WorldPosition : IComponent { public Vector3 Value; }
   ```

2. Даёте Unity скомпилировать — Roslyn читает `Assembly-CSharp.csproj`, он обновляется при изменении скриптов.
3. Горячая клавиша в VS.
4. Переключаетесь в Unity — она подхватит `Assets/Generated`.

Получаете `entity.worldPosition`, `AddWorldPosition(...)`, `ReplaceWorldPosition(...)`, `GameMatcher.WorldPosition`.

### Первый запуск на пустом проекте — две генерации

Атрибут `[Game]` **генерируется**, а не пишется руками. На чистом проекте его ещё нет, поэтому:

1. Первая генерация — создаются `GameAttribute.cs`, `GameContext.cs`, `GameEntity.cs`, `GameMatcher.cs`, `Contexts.cs`, `Feature.cs`.
2. Теперь `[Game]` существует — помечаете им компоненты.
3. Вторая генерация — достраивается API по компонентам.

---

## Чего не делать

- **Не открывать `Tools → Jenny → Preferences`.** Окно держит конфиг в памяти и по `Generate` перезаписывает `Jenny.properties` целиком, затирая правки в файле. Если всё же открыли и что-то сломалось — сверьте `SearchPaths` с эталоном из шага 3.
- **Не нажимать `Auto Import`.** Он сканирует папки и добавляет в `SearchPaths` путь `Assets\Entitas\Jenny\Editor\Jenny` с дубликатами плагинов — генерация после этого падает.
- **Не нажимать `Generate` в Unity** без запущенного Jenny-сервера. В 1.14.1 внутрипроцессная генерация не работает.
- **Не править файлы в `Assets/Generated`.** `CleanTargetDirectoryPostProcessor` стирает папку при каждой генерации.

---

## Git

| Путь | Действие |
|---|---|
| `Jenny.properties` | коммитить |
| `Jenny-*.bat` | коммитить |
| `Jenny/` (папка с dll) | коммитить, если хотите генерацию без ручной доустановки |
| `*.userproperties` | в `.gitignore` — машинно-специфичный |
| `Entitas.properties` | коммитить, генерируется Unity под VisualDebugging |
| `Assets/Generated/` | на усмотрение: код восстановимый, но без него проект не соберётся у того, кто не запускал генерацию |

---

## Диагностика

Первым делом:

```powershell
dotnet Jenny\Jenny.Generator.Cli.dll doctor
```

Полный стектрейс вместо короткого сообщения:

```powershell
dotnet Jenny\Jenny.Generator.Cli.dll doctor -v
dotnet Jenny\Jenny.Generator.Cli.dll gen -v
```

### Частые ошибки

**`Could not find a part of the path "...\Jenny.properties"`**
Unity смотрит по пути из реестра, доставшемуся от другого проекта. См. шаг 1.

**`Could not resolve type with token ... 'System.Threading.CancellationToken' in assembly 'System.Runtime, Version=6.0.0.0'`**
net6-сборки грузятся в Mono-процесс Unity. Значит генерация идёт внутри Unity — не жмите кнопку `Generate`, используйте `.bat` или горячую клавишу из VS.

**`Could not load file or assembly 'Entitas, Version=1.0.0.0'`**
В `SearchPaths` нет `Assets\Entitas\Entitas`. См. шаг 3.

**`Unused key: ...` для всех ключей сразу**
Плагины не загрузились вообще. Смотрите `doctor -v` — там будет настоящая причина ниже по выводу.

**`... uses Unity APIs but is used outside of Unity!`**
В конфиге остались `Jenny.Plugins.Unity.*` препроцессоры или постпроцессоры. Убрать.

**Пустой результат генерации при наличии компонентов**
Не обновился `Assembly-CSharp.csproj`. В Unity: `Edit → Preferences → External Tools → Regenerate project files`.

**`Неверный дескриптор` при запуске CLI**
CLI требует настоящую консоль. Запускайте через `.bat` или из окна терминала, а не из скриптов с перенаправленным вводом.

---

## Приложение: кнопка `Generate` в Unity через сервер

Если всё же хочется жать кнопку в редакторе.

1. Запустить `Jenny-Server.bat`, окно оставить открытым (слушает порт 3333).
2. В Unity: `Tools → Jenny → Preferences` → галка **`Use Jenny Server`**.
3. `Generate` — Unity отправит запрос серверу, генерацию выполнит внешний net6-процесс.

Минусы: сервер нужно держать запущенным, а окно Jenny по-прежнему перезаписывает `Jenny.properties` своим состоянием. Вариант с горячей клавишей из VS надёжнее.
