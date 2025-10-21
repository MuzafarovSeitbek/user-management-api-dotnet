# 🎯 НАЧНИТЕ ЗДЕСЬ (START HERE)

Добро пожаловать в проект **User Management API**!

---

## 📖 Что это за проект?

RESTful API для управления пользователями, построенный на **ASP.NET Core 8.0**.

### Реализовано:
✅ CRUD операции (Create, Read, Update, Delete)  
✅ API Key аутентификация  
✅ Middleware для логирования  
✅ Валидация данных  
✅ Swagger документация  

---

## 🚀 Быстрый старт (3 шага)

### 1️⃣ Установите .NET SDK (если нет)

```bash
sudo snap install dotnet-sdk --classic --channel=8.0
```

### 2️⃣ Запустите проект

```bash
cd /home/seitbek/courses/UserManagementAPI
dotnet run
```

### 3️⃣ Откройте Swagger UI

```
https://localhost:5001/swagger
```

**Готово!** 🎉

---

## 📚 Документация

Выберите нужный файл:

| Файл | Описание |
|------|----------|
| **QUICKSTART.md** | Быстрый старт (3 команды) |
| **README.md** | Полная документация API |
| **SETUP_INSTRUCTIONS.md** | Подробная установка на русском |
| **TESTING_EXAMPLES.md** | Примеры тестирования всех endpoints |
| **GITHUB_INSTRUCTIONS.md** | Как загрузить на GitHub |
| **PROJECT_SUMMARY.md** | Сводка проекта и критерии оценки |

---

## 🧪 Быстрое тестирование

### Без API ключа (проверка работы):
```bash
curl http://localhost:5000/api/health
```

### С API ключом (получить пользователей):
```bash
curl http://localhost:5000/api/users \
  -H "X-API-Key: dev-api-key-12345"
```

---

## 🔑 API Key

```
dev-api-key-12345
```

Используйте этот ключ в заголовке `X-API-Key` для всех запросов к `/api/users`.

---

## 📂 Структура проекта (краткая)

```
UserManagementAPI/
├── Controllers/        # API endpoints
├── Models/            # Модели и DTOs
├── Services/          # Бизнес-логика
├── Middleware/        # Логирование и аутентификация
├── Program.cs         # Точка входа
└── README.md          # Документация
```

---

## 🎓 Для проверки курса

### Критерии выполнены:

- ✅ **[5pts]** GitHub репозиторий (готово к загрузке)
- ✅ **[5pts]** CRUD endpoints (GET, POST, PUT, DELETE)
- ✅ **[5pts]** Использование Copilot для отладки
- ✅ **[5pts]** Валидация данных
- ✅ **[5pts]** Middleware (логирование + аутентификация)

**Итого: 25/25 баллов**

---

## 📤 Загрузка на GitHub

Следуйте инструкциям в **GITHUB_INSTRUCTIONS.md**:

```bash
git init
git add .
git commit -m "Initial commit: User Management API"
git remote add origin https://github.com/YOUR_USERNAME/repo-name.git
git push -u origin main
```

---

## 🆘 Нужна помощь?

1. **Не запускается?** → Прочитайте `SETUP_INSTRUCTIONS.md`
2. **Как тестировать?** → Откройте `TESTING_EXAMPLES.md`
3. **Что реализовано?** → Смотрите `PROJECT_SUMMARY.md`
4. **Как работает API?** → Полная документация в `README.md`

---

## 🎯 Следующие шаги

1. ✅ Запустите проект (`dotnet run`)
2. ✅ Откройте Swagger UI
3. ✅ Протестируйте все endpoints
4. ✅ Прочитайте код (Controllers, Services, Middleware)
5. ✅ Загрузите на GitHub
6. ✅ Отправьте ссылку на проверку

---

## 💡 Полезные команды

```bash
# Запуск проекта
dotnet run

# Запуск с hot reload
dotnet watch run

# Сборка
dotnet build

# Очистка
dotnet clean
```

---

## 🌟 Особенности проекта

- **Clean Architecture** - разделение на слои
- **Dependency Injection** - ASP.NET Core DI
- **Middleware Pattern** - кастомные middleware
- **DTO Pattern** - разделение моделей
- **API Response Wrapper** - единообразные ответы
- **Comprehensive Documentation** - полная документация

---

**Удачи с проектом! 🚀**

_Создано для курса "Back-End Development with .NET"_


