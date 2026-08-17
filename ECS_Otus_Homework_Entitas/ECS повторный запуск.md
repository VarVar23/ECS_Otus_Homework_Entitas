# ECS: повторный запуск

Быстрая установка Entitas 1.14.1 + Jenny в новый проект, когда шаблон уже собран.

Подробности и объяснения — в [ECS первый запуск.md](ECS%20первый%20запуск.md). Здесь только последовательность действий.

## Что должно быть в шаблоне

```
Шаблон/
├── Entitas/                 ← из архива Entitas 1.14.1, папка Entitas
├── Jenny/                   ← из архива, папка Jenny/Jenny
├── Jenny.properties
├── Jenny-Generate.bat
└── Jenny-Doctor.bat
```

Если чего-то нет — см. шаги 2-4 первой инструкции.

---

## 1. Player Settings

`Edit → Project Settings → Player` → задать свои **Company Name** и **Product Name**.

Делать до открытия окна Jenny. Иначе проект унаследует путь к `Jenny.properties` из реестра от предыдущего проекта с теми же именами.

## 2. Скопировать файлы

| Из шаблона | Куда |
|---|---|
| `Entitas/` | `<Проект>/Assets/Entitas/` |
| `Jenny/` | `<Проект>/Jenny/` — в корень, рядом с `Assets` |
| `Jenny.properties` | в корень |
| `Jenny-Generate.bat`, `Jenny-Doctor.bat` | в корень |

## 3. Поправить контексты

Единственная строка в `Jenny.properties`, которая меняется под проект:

```properties
Entitas.CodeGeneration.Plugins.Contexts = Game, \
                                          Input
```

Всё остальное — пути относительные, переносится как есть.

## 4. Настроить горячую клавишу в Visual Studio

Делается один раз **на машину**, а не на проект — если уже настроено, пропустить.

`Tools → External Tools → Add`:

| Поле | Значение |
|---|---|
| Title | `Jenny Generate` |
| Command | `C:\Program Files\dotnet\dotnet.exe` |
| Arguments | `Jenny\Jenny.Generator.Cli.dll gen` |
| Initial directory | `$(SolutionDir)` |
| Use Output window | ☑ |

Затем `Tools → Options → Environment → Keyboard` → команда `Tools.ExternalCommand<N>`, где `N` — позиция в списке External Tools → назначить сочетание.

## 5. Первая генерация

1. Открыть Unity, дождаться компиляции.
2. Двойной клик по `Jenny-Generate.bat` (или горячая клавиша в VS).
3. Должно появиться `Assets/Generated/` с `Contexts.cs`, `Feature.cs` и папками контекстов.

Атрибуты `[Game]`, `[Input]` появляются именно на этом шаге — руками они не пишутся.

## 6. Компоненты

```csharp
using Entitas;
using UnityEngine;

[Game] public class WorldPosition : IComponent { public Vector3 Value; }
```

Дать Unity скомпилировать → сгенерировать ещё раз. Появится `entity.worldPosition`, `AddWorldPosition(...)`, `GameMatcher.WorldPosition`.

---

## Чего не делать

- не открывать `Tools → Jenny → Preferences` — окно перезапишет `Jenny.properties`
- не нажимать `Auto Import` — сломает `SearchPaths`
- не нажимать `Generate` в Unity — в 1.14.1 не работает без сервера
- не править файлы в `Assets/Generated` — стираются при каждой генерации

## Если что-то пошло не так

```powershell
dotnet Jenny\Jenny.Generator.Cli.dll doctor -v
```

| Ошибка | Причина |
|---|---|
| `Could not find a part of the path "...\Jenny.properties"` | путь из реестра от другого проекта — шаг 1 |
| `... 'System.Threading.CancellationToken' in assembly 'System.Runtime, Version=6.0.0.0'` | генерация запущена внутри Unity — используйте `.bat` |
| `Could not load file or assembly 'Entitas, Version=1.0.0.0'` | в `SearchPaths` нет `Assets\Entitas\Entitas` |
| `Unused key` по всем ключам сразу | плагины не загрузились, смотрите `doctor -v` |
| генерация пустая при наличии компонентов | не обновился `Assembly-CSharp.csproj` — `Edit → Preferences → External Tools → Regenerate project files` |

Полный разбор причин — в [ECS первый запуск.md](ECS%20первый%20запуск.md).
