# Инструкция по установке и запуску (Setup Instructions)

## Установка .NET SDK

Поскольку .NET SDK не установлен на вашей системе, выполните следующие шаги:

### Вариант 1: Установка через snap (Рекомендуется)

```bash
# Установка .NET SDK 8.0
sudo snap install dotnet-sdk --classic --channel=8.0

# Проверка установки
dotnet --version
```

### Вариант 2: Установка через apt

```bash
# Установка .NET SDK 8.0
sudo apt update
sudo apt install -y dotnet-sdk-8.0

# Проверка установки
dotnet --version
```

## Запуск проекта

После установки .NET SDK:

### 1. Перейдите в директорию проекта

```bash
cd /home/seitbek/courses/UserManagementAPI
```

### 2. Восстановите зависимости

```bash
dotnet restore
```

### 3. Соберите проект

```bash
dotnet build
```

### 4. Запустите приложение

```bash
dotnet run
```

### 5. Откройте Swagger UI в браузере

```
https://localhost:5001/swagger
```

## Быстрое тестирование

### Проверка работы API (без аутентификации):

```bash
curl http://localhost:5000/api/health
```

### Получение всех пользователей (с API ключом):

```bash
curl -X GET http://localhost:5000/api/users \
  -H "X-API-Key: dev-api-key-12345"
```

### Создание нового пользователя:

```bash
curl -X POST http://localhost:5000/api/users \
  -H "X-API-Key: dev-api-key-12345" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "testuser",
    "email": "test@example.com",
    "firstName": "Test",
    "lastName": "User",
    "age": 25
  }'
```

## Структура проекта

Проект создан со следующей структурой:

```
UserManagementAPI/
├── Controllers/              # API контроллеры
│   ├── UsersController.cs   # CRUD операции для пользователей
│   └── HealthController.cs  # Health check endpoint
├── Models/                   # Модели данных
│   ├── User.cs              # Модель пользователя
│   ├── ApiResponse.cs       # Обертка для ответов
│   └── DTOs/                # Data Transfer Objects
│       ├── CreateUserDto.cs
│       └── UpdateUserDto.cs
├── Services/                 # Бизнес-логика
│   ├── IUserService.cs      # Интерфейс сервиса
│   └── UserService.cs       # Реализация (in-memory)
├── Middleware/               # Middleware компоненты
│   ├── RequestLoggingMiddleware.cs
│   └── ApiKeyAuthenticationMiddleware.cs
├── Properties/
│   └── launchSettings.json  # Настройки запуска
├── appsettings.json         # Конфигурация
├── Program.cs               # Точка входа
└── UserManagementAPI.csproj # Файл проекта
```

## Особенности реализации

### ✅ Выполнены все требования курса:

1. **CRUD endpoints** (GET, POST, PUT, DELETE) - реализованы в `UsersController.cs`
2. **Middleware для логирования** - `RequestLoggingMiddleware.cs`
3. **Middleware для аутентификации** - `ApiKeyAuthenticationMiddleware.cs`
4. **Валидация данных** - через Data Annotations в DTOs
5. **Обработка ошибок** - во всех контроллерах

### Дополнительные возможности:

- **Swagger/OpenAPI** документация
- **Dependency Injection** (ASP.NET Core DI)
- **DTO Pattern** для разделения моделей
- **Structured Logging** через ILogger
- **CORS** поддержка
- **Health Check** endpoint

## API Key для тестирования

По умолчанию используется следующий API ключ:

```
dev-api-key-12345
```

Вы можете изменить его в файле `appsettings.Development.json`.

## Решение проблем

### Проблема: "dotnet: command not found"

**Решение:** Установите .NET SDK (см. начало этого файла).

### Проблема: "Unable to configure HTTPS endpoint"

**Решение:** Доверьте сертификату разработки:
```bash
dotnet dev-certs https --trust
```

### Проблема: Порт уже используется

**Решение:** Измените порт в `Properties/launchSettings.json` или остановите процесс:
```bash
sudo lsof -i :5000
sudo kill -9 <PID>
```

## Тестирование в Postman

1. Импортируйте endpoints в Postman
2. Создайте переменные окружения:
   - `base_url`: `http://localhost:5000`
   - `api_key`: `dev-api-key-12345`
3. Добавьте заголовок `X-API-Key: {{api_key}}` ко всем запросам
4. Протестируйте каждый endpoint

## Следующие шаги

После запуска проекта:

1. ✅ Протестируйте все endpoints через Swagger UI
2. ✅ Проверьте валидацию данных
3. ✅ Убедитесь, что middleware работает (логи в консоли)
4. ✅ Создайте GitHub репозиторий
5. ✅ Загрузите код на GitHub
6. ✅ Отправьте ссылку на проверку

## Полезные команды

```bash
# Восстановить зависимости
dotnet restore

# Собрать проект
dotnet build

# Запустить проект
dotnet run

# Запустить с hot reload
dotnet watch run

# Очистить build artifacts
dotnet clean

# Опубликовать для production
dotnet publish -c Release -o ./publish
```

## Документация

- README.md - Полная документация API
- Swagger UI - Интерактивная документация (запустите проект и откройте /swagger)
- Этот файл - Инструкции по установке и настройке


