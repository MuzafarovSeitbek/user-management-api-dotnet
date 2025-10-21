# Инструкция по загрузке проекта на GitHub

## Шаг 1: Инициализация Git репозитория

```bash
# Перейдите в директорию проекта
cd /home/seitbek/courses/UserManagementAPI

# Инициализируйте Git репозиторий
git init

# Добавьте все файлы
git add .

# Сделайте первый коммит
git commit -m "Initial commit: User Management API with .NET"
```

## Шаг 2: Создание репозитория на GitHub

1. Откройте браузер и перейдите на [GitHub](https://github.com)
2. Войдите в свой аккаунт
3. Нажмите на кнопку **"+"** в правом верхнем углу
4. Выберите **"New repository"**
5. Заполните форму:
   - **Repository name**: `user-management-api-dotnet`
   - **Description**: `A RESTful API for user management built with ASP.NET Core`
   - **Visibility**: Public (для проверки курса)
   - **НЕ** выбирайте "Initialize with README" (у нас уже есть файлы)
6. Нажмите **"Create repository"**

## Шаг 3: Подключение локального репозитория к GitHub

После создания репозитория GitHub покажет инструкции. Выполните:

```bash
# Добавьте remote origin (замените YOUR_USERNAME на ваше имя пользователя)
git remote add origin https://github.com/YOUR_USERNAME/user-management-api-dotnet.git

# Проверьте, что remote добавлен
git remote -v

# Переименуйте ветку в main (если нужно)
git branch -M main

# Загрузите код на GitHub
git push -u origin main
```

## Шаг 4: Проверка загрузки

1. Обновите страницу вашего репозитория на GitHub
2. Убедитесь, что все файлы загружены
3. Проверьте, что README.md отображается корректно

## Шаг 5: Отправка ссылки на проверку

Скопируйте URL вашего репозитория:

```
https://github.com/YOUR_USERNAME/user-management-api-dotnet
```

Отправьте эту ссылку на проверку согласно инструкциям курса.

## Что должно быть в репозитории

✅ Все исходные файлы проекта  
✅ README.md с документацией  
✅ .gitignore (бинарные файлы не должны быть загружены)  
✅ Четкая структура проекта  
✅ Описание всех endpoints  

## Структура файлов (для проверки)

```
UserManagementAPI/
├── Controllers/
│   ├── UsersController.cs
│   └── HealthController.cs
├── Models/
│   ├── User.cs
│   ├── ApiResponse.cs
│   └── DTOs/
│       ├── CreateUserDto.cs
│       └── UpdateUserDto.cs
├── Services/
│   ├── IUserService.cs
│   └── UserService.cs
├── Middleware/
│   ├── RequestLoggingMiddleware.cs
│   └── ApiKeyAuthenticationMiddleware.cs
├── Properties/
│   └── launchSettings.json
├── appsettings.json
├── appsettings.Development.json
├── Program.cs
├── UserManagementAPI.csproj
├── .gitignore
├── README.md
├── SETUP_INSTRUCTIONS.md
└── GITHUB_INSTRUCTIONS.md
```

## Критерии оценки (по заданию)

Проверьте, что ваш проект соответствует требованиям:

- ✅ **[5pts]** Создан GitHub репозиторий
- ✅ **[5pts]** Код включает CRUD endpoints (GET, POST, PUT, DELETE)
- ✅ **[5pts]** Использован Copilot для отладки (если применимо)
- ✅ **[5pts]** Код включает дополнительную функциональность (валидация)
- ✅ **[5pts]** Реализован middleware (логирование и аутентификация)

## Дополнительные команды Git

### Проверка статуса

```bash
git status
```

### Добавление новых изменений

```bash
git add .
git commit -m "Описание изменений"
git push
```

### Просмотр истории

```bash
git log --oneline
```

### Создание .gitignore (уже создан)

Файл `.gitignore` уже включен в проект и исключает:
- `bin/` и `obj/` директории
- `.vs/` и `.vscode/`
- Зависимости и временные файлы

## Решение проблем

### Проблема: "remote origin already exists"

```bash
git remote remove origin
git remote add origin https://github.com/YOUR_USERNAME/user-management-api-dotnet.git
```

### Проблема: "Permission denied"

Возможно, нужно настроить SSH ключ или использовать Personal Access Token.

**Использование Personal Access Token:**

1. Перейдите на GitHub → Settings → Developer settings → Personal access tokens
2. Создайте новый token с правами `repo`
3. Используйте token вместо пароля при push

### Проблема: "Large files"

Если Git жалуется на большие файлы, убедитесь что `.gitignore` правильно настроен.

```bash
# Удалить файлы из staging area
git rm --cached -r bin/
git rm --cached -r obj/

# Добавить в .gitignore и закоммитить
git add .gitignore
git commit -m "Update .gitignore"
git push
```

## README на GitHub

После загрузки, GitHub автоматически отобразит ваш `README.md` файл на главной странице репозитория. Убедитесь, что он содержит:

1. **Описание проекта**
2. **Инструкции по установке**
3. **Список endpoints**
4. **Примеры использования**
5. **Технологии**

## Финальная проверка

Перед отправкой на проверку:

1. ✅ Репозиторий публичный
2. ✅ README.md хорошо отформатирован
3. ✅ Все файлы загружены
4. ✅ .gitignore работает корректно (нет bin/obj)
5. ✅ Код компилируется (`dotnet build`)
6. ✅ Проект запускается (`dotnet run`)
7. ✅ Endpoints работают (протестированы)

## Пример хорошего README на GitHub

Ваш README уже содержит все необходимое:

- Описание проекта и функций
- Эмодзи для лучшего восприятия
- Структура проекта
- Инструкции по установке
- Документация API
- Примеры использования
- Информация о технологиях

## Дополнительные улучшения (опционально)

Для получения дополнительных баллов, вы можете:

1. Добавить скриншоты Swagger UI в README
2. Создать GitHub Actions для CI/CD
3. Добавить badges (build status, version)
4. Создать Wiki страницы
5. Добавить примеры тестов

Удачи с проверкой! 🚀

