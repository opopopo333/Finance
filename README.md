# Личные финансы (УП.01.01)

Учебная практика УП.01.01. Тема: «Проектирование и разработка базы данных для сервиса по управлению личными финансами и бюджетом».

Автор: Ульяненко.

## Стек

- Microsoft SQL Server (Express) + SSMS
- WPF, .NET Framework 4.7.2
- Entity Framework 6, Database First (ADO.NET Entity Data Model, `.edmx`)
- Microsoft Office Interop (Word, Excel) для отчётов

## База данных

БД `DB_Ulianenko_Finance`, 3 таблицы:

| Таблица | Назначение |
|---|---|
| `Users` | пользователи: логин, хэш пароля (SHA-256), роль, ФИО, фото, тема |
| `Category` | категории операций, тип «Доход» / «Расход» |
| `Operation` | операции пользователя: дата, название, сумма, комментарий |

`Operation` связана с `Users` и `Category` внешними ключами с каскадным удалением.

### Как развернуть

1. Открыть SSMS и подключиться к `.\SQLEXPRESS`.
2. Открыть и выполнить скрипт `db/schema.sql`.

## Как запустить

1. Открыть `Finance.slnx` в Visual Studio.
2. Проверить строку подключения `DB_Ulianenko_FinanceEntities` в `Finance/App.config`.
3. Запустить проект (F5).

## Тестовые пользователи

| Логин | Пароль | Роль |
|---|---|---|
| admin | Admin123 | Admin |
| manager | Manager1 | Admin |
| ivanov | Ivanov12 | User |
| petrova | Petrova1 | User |
| sidorov | Sidorov1 | User |

## Структура решения

```
README.md          описание работы
db/schema.sql      скрипт создания БД и тестовые данные
res/               иконка, фото пользователей, шрифт
Finance.slnx       решение
Finance/           проект WPF
    Model1.edmx    модель EF и сгенерированные классы
```

## Страницы и функции

_Раздел будет дополнен по мере разработки._
