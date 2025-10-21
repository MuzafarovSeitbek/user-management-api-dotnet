# 🚀 Quick Start Guide

## Если .NET SDK не установлен

```bash
# Установите .NET SDK 8.0
sudo snap install dotnet-sdk --classic --channel=8.0

# Проверьте установку
dotnet --version
```

## Запуск проекта за 3 шага

### 1️⃣ Перейдите в директорию проекта

```bash
cd /home/seitbek/courses/UserManagementAPI
```

### 2️⃣ Восстановите зависимости и соберите проект

```bash
dotnet build
```

### 3️⃣ Запустите приложение

```bash
dotnet run
```

✅ **Готово!** API запущен на:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger: `https://localhost:5001/swagger`

---

## Быстрое тестирование

### Проверка работы (без API ключа):

```bash
curl http://localhost:5000/api/health
```

**Ожидаемый результат:**
```json
{
  "message": "Welcome to User Management API",
  "version": "1.0.0",
  "status": "healthy",
  ...
}
```

### Получить всех пользователей:

```bash
curl -X GET http://localhost:5000/api/users \
  -H "X-API-Key: dev-api-key-12345"
```

### Создать пользователя:

```bash
curl -X POST http://localhost:5000/api/users \
  -H "X-API-Key: dev-api-key-12345" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "newuser",
    "email": "newuser@example.com",
    "firstName": "New",
    "lastName": "User",
    "age": 25
  }'
```

---

## Использование Swagger UI (Самый простой способ!)

1. Откройте браузер
2. Перейдите на: `https://localhost:5001/swagger`
3. Нажмите **"Authorize"** (зеленая кнопка справа)
4. Введите API ключ: `dev-api-key-12345`
5. Нажмите **"Authorize"** и **"Close"**
6. Теперь тестируйте все endpoints прямо из браузера! 🎉

---

## Структура endpoints

| Метод | URL | Описание |
|-------|-----|----------|
| GET | `/api/health` | Проверка работы API |
| GET | `/api/users` | Получить всех пользователей |
| GET | `/api/users/{id}` | Получить пользователя по ID |
| POST | `/api/users` | Создать пользователя |
| PUT | `/api/users/{id}` | Обновить пользователя |
| DELETE | `/api/users/{id}` | Удалить пользователя |

**Важно:** Все endpoints кроме `/api/health` требуют заголовок `X-API-Key: dev-api-key-12345`

---

## Что дальше?

1. ✅ Изучите код в папках `Controllers/`, `Services/`, `Middleware/`
2. ✅ Протестируйте все endpoints через Swagger
3. ✅ Прочитайте полную документацию в `README.md`
4. ✅ Следуйте инструкциям в `GITHUB_INSTRUCTIONS.md` для загрузки на GitHub

---

## Полезные команды

```bash
# Запуск с auto-reload при изменении кода
dotnet watch run

# Сборка Release версии
dotnet build -c Release

# Публикация для деплоя
dotnet publish -c Release -o ./publish

# Очистка build файлов
dotnet clean
```

---

## Остановка приложения

Нажмите `Ctrl + C` в терминале где запущено приложение.

---

## Помощь

Если возникли проблемы:
1. Прочитайте `SETUP_INSTRUCTIONS.md` для подробных инструкций
2. Проверьте, что .NET SDK установлен: `dotnet --version`
3. Убедитесь, что порты 5000/5001 свободны
4. Проверьте логи в консоли

---

**Готово к использованию! Приятной разработки! 🎯**

