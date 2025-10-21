# 🧪 Testing Examples

Примеры тестирования всех endpoints API.

## API Key для тестов

```
dev-api-key-12345
```

---

## 1. Health Check (без аутентификации)

### cURL:
```bash
curl http://localhost:5000/api/health
```

### PowerShell:
```powershell
Invoke-RestMethod -Uri "http://localhost:5000/api/health" -Method Get
```

### Ожидаемый ответ (200 OK):
```json
{
  "message": "Welcome to User Management API",
  "version": "1.0.0",
  "status": "healthy",
  "timestamp": "2024-01-19T12:00:00Z",
  "endpoints": {
    "users": "/api/users",
    "swagger": "/swagger",
    "health": "/api/health"
  }
}
```

---

## 2. Получить всех пользователей

### cURL:
```bash
curl -X GET http://localhost:5000/api/users \
  -H "X-API-Key: dev-api-key-12345"
```

### PowerShell:
```powershell
$headers = @{
    "X-API-Key" = "dev-api-key-12345"
}
Invoke-RestMethod -Uri "http://localhost:5000/api/users" -Method Get -Headers $headers
```

### JavaScript (Fetch):
```javascript
fetch('http://localhost:5000/api/users', {
  method: 'GET',
  headers: {
    'X-API-Key': 'dev-api-key-12345'
  }
})
.then(response => response.json())
.then(data => console.log(data));
```

### C# (HttpClient):
```csharp
using var client = new HttpClient();
client.DefaultRequestHeaders.Add("X-API-Key", "dev-api-key-12345");
var response = await client.GetAsync("http://localhost:5000/api/users");
var content = await response.Content.ReadAsStringAsync();
Console.WriteLine(content);
```

### Ожидаемый ответ (200 OK):
```json
{
  "success": true,
  "message": null,
  "data": [
    {
      "id": "guid-1",
      "username": "johndoe",
      "email": "john@example.com",
      "firstName": "John",
      "lastName": "Doe",
      "age": 30,
      "createdAt": "2024-01-19T12:00:00Z",
      "updatedAt": null
    },
    {
      "id": "guid-2",
      "username": "janedoe",
      "email": "jane@example.com",
      "firstName": "Jane",
      "lastName": "Doe",
      "age": 28,
      "createdAt": "2024-01-19T12:00:00Z",
      "updatedAt": null
    }
  ],
  "count": 2
}
```

---

## 3. Получить пользователя по ID

**Сначала получите ID пользователя из предыдущего запроса!**

### cURL:
```bash
# Замените {USER_ID} на реальный GUID
curl -X GET http://localhost:5000/api/users/{USER_ID} \
  -H "X-API-Key: dev-api-key-12345"
```

### Ожидаемый ответ (200 OK):
```json
{
  "success": true,
  "message": null,
  "data": {
    "id": "guid-here",
    "username": "johndoe",
    "email": "john@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "age": 30,
    "createdAt": "2024-01-19T12:00:00Z",
    "updatedAt": null
  },
  "count": null
}
```

### Ошибка (404 Not Found):
```bash
curl -X GET http://localhost:5000/api/users/00000000-0000-0000-0000-000000000000 \
  -H "X-API-Key: dev-api-key-12345"
```

```json
{
  "error": "Not Found",
  "message": "User with ID 00000000-0000-0000-0000-000000000000 not found",
  "details": null
}
```

---

## 4. Создать пользователя

### cURL:
```bash
curl -X POST http://localhost:5000/api/users \
  -H "X-API-Key: dev-api-key-12345" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "bobsmith",
    "email": "bob@example.com",
    "firstName": "Bob",
    "lastName": "Smith",
    "age": 35
  }'
```

### PowerShell:
```powershell
$headers = @{
    "X-API-Key" = "dev-api-key-12345"
    "Content-Type" = "application/json"
}

$body = @{
    username = "bobsmith"
    email = "bob@example.com"
    firstName = "Bob"
    lastName = "Smith"
    age = 35
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/api/users" `
  -Method Post -Headers $headers -Body $body
```

### JavaScript (Fetch):
```javascript
fetch('http://localhost:5000/api/users', {
  method: 'POST',
  headers: {
    'X-API-Key': 'dev-api-key-12345',
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    username: 'bobsmith',
    email: 'bob@example.com',
    firstName: 'Bob',
    lastName: 'Smith',
    age: 35
  })
})
.then(response => response.json())
.then(data => console.log(data));
```

### Ожидаемый ответ (201 Created):
```json
{
  "success": true,
  "message": "User created successfully",
  "data": {
    "id": "new-guid",
    "username": "bobsmith",
    "email": "bob@example.com",
    "firstName": "Bob",
    "lastName": "Smith",
    "age": 35,
    "createdAt": "2024-01-19T13:00:00Z",
    "updatedAt": null
  },
  "count": null
}
```

---

## 5. Тестирование валидации

### Невалидные данные:
```bash
curl -X POST http://localhost:5000/api/users \
  -H "X-API-Key: dev-api-key-12345" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "ab",
    "email": "not-an-email",
    "firstName": "Test123",
    "lastName": "",
    "age": 200
  }'
```

### Ожидаемый ответ (400 Bad Request):
```json
{
  "error": "Validation Error",
  "message": "Invalid user data",
  "details": [
    "Username must be between 3 and 20 characters",
    "Invalid email format",
    "First name can only contain letters, spaces, hyphens, and apostrophes",
    "Last name is required",
    "Age must be between 0 and 150"
  ]
}
```

---

## 6. Тестирование дубликатов

### Попытка создать пользователя с существующим username:
```bash
curl -X POST http://localhost:5000/api/users \
  -H "X-API-Key: dev-api-key-12345" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "johndoe",
    "email": "different@example.com",
    "firstName": "John",
    "lastName": "Smith",
    "age": 25
  }'
```

### Ожидаемый ответ (409 Conflict):
```json
{
  "error": "Conflict",
  "message": "Username already exists",
  "details": null
}
```

---

## 7. Обновить пользователя

**Используйте ID созданного пользователя!**

### cURL:
```bash
curl -X PUT http://localhost:5000/api/users/{USER_ID} \
  -H "X-API-Key: dev-api-key-12345" \
  -H "Content-Type: application/json" \
  -d '{
    "age": 36
  }'
```

### Обновить несколько полей:
```bash
curl -X PUT http://localhost:5000/api/users/{USER_ID} \
  -H "X-API-Key: dev-api-key-12345" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "newemail@example.com",
    "age": 36
  }'
```

### Ожидаемый ответ (200 OK):
```json
{
  "success": true,
  "message": "User updated successfully",
  "data": {
    "id": "user-guid",
    "username": "bobsmith",
    "email": "newemail@example.com",
    "firstName": "Bob",
    "lastName": "Smith",
    "age": 36,
    "createdAt": "2024-01-19T13:00:00Z",
    "updatedAt": "2024-01-19T14:30:00Z"
  },
  "count": null
}
```

---

## 8. Удалить пользователя

### cURL:
```bash
curl -X DELETE http://localhost:5000/api/users/{USER_ID} \
  -H "X-API-Key: dev-api-key-12345"
```

### Ожидаемый ответ (200 OK):
```json
{
  "success": true,
  "message": "User deleted successfully",
  "data": {
    "id": "user-guid",
    "username": "bobsmith",
    "email": "bob@example.com",
    "firstName": "Bob",
    "lastName": "Smith",
    "age": 36,
    "createdAt": "2024-01-19T13:00:00Z",
    "updatedAt": "2024-01-19T14:30:00Z"
  },
  "count": null
}
```

---

## 9. Тестирование аутентификации

### Без API ключа (401 Unauthorized):
```bash
curl -X GET http://localhost:5000/api/users
```

```json
{
  "error": "Unauthorized",
  "message": "API key is required. Please provide X-API-Key header.",
  "details": null
}
```

### С неверным API ключом (403 Forbidden):
```bash
curl -X GET http://localhost:5000/api/users \
  -H "X-API-Key: wrong-key"
```

```json
{
  "error": "Forbidden",
  "message": "Invalid API key.",
  "details": null
}
```

---

## 10. Полный сценарий тестирования

```bash
# 1. Проверка работы
curl http://localhost:5000/api/health

# 2. Получить всех пользователей
curl -X GET http://localhost:5000/api/users \
  -H "X-API-Key: dev-api-key-12345"

# 3. Создать нового пользователя
curl -X POST http://localhost:5000/api/users \
  -H "X-API-Key: dev-api-key-12345" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "alice",
    "email": "alice@example.com",
    "firstName": "Alice",
    "lastName": "Wonder",
    "age": 28
  }'

# Сохраните ID из ответа!

# 4. Получить созданного пользователя по ID
curl -X GET http://localhost:5000/api/users/{ALICE_ID} \
  -H "X-API-Key: dev-api-key-12345"

# 5. Обновить пользователя
curl -X PUT http://localhost:5000/api/users/{ALICE_ID} \
  -H "X-API-Key: dev-api-key-12345" \
  -H "Content-Type: application/json" \
  -d '{"age": 29}'

# 6. Удалить пользователя
curl -X DELETE http://localhost:5000/api/users/{ALICE_ID} \
  -H "X-API-Key: dev-api-key-12345"

# 7. Проверить, что пользователь удален (должно вернуть 404)
curl -X GET http://localhost:5000/api/users/{ALICE_ID} \
  -H "X-API-Key: dev-api-key-12345"
```

---

## Тестирование в Postman

### Настройка Environment:

1. Создайте новое окружение "Local Development"
2. Добавьте переменные:
   ```
   base_url = http://localhost:5000
   api_key = dev-api-key-12345
   user_id = (будет заполнено автоматически)
   ```

### Автоматическое сохранение ID:

В разделе "Tests" запроса Create User добавьте:

```javascript
if (pm.response.code === 201) {
    var jsonData = pm.response.json();
    pm.environment.set("user_id", jsonData.data.id);
}
```

Теперь ID автоматически сохранится в переменную `{{user_id}}`.

---

## Python скрипт для полного тестирования

```python
import requests
import json

BASE_URL = "http://localhost:5000"
API_KEY = "dev-api-key-12345"
headers = {"X-API-Key": API_KEY, "Content-Type": "application/json"}

print("1. Health Check")
response = requests.get(f"{BASE_URL}/api/health")
print(f"Status: {response.status_code}")
print(json.dumps(response.json(), indent=2))

print("\n2. Get All Users")
response = requests.get(f"{BASE_URL}/api/users", headers={"X-API-Key": API_KEY})
print(f"Status: {response.status_code}")
print(f"Count: {response.json()['count']}")

print("\n3. Create User")
new_user = {
    "username": "testuser",
    "email": "test@example.com",
    "firstName": "Test",
    "lastName": "User",
    "age": 25
}
response = requests.post(f"{BASE_URL}/api/users", headers=headers, json=new_user)
print(f"Status: {response.status_code}")
user_id = response.json()['data']['id']
print(f"Created User ID: {user_id}")

print("\n4. Get User by ID")
response = requests.get(f"{BASE_URL}/api/users/{user_id}", headers={"X-API-Key": API_KEY})
print(f"Status: {response.status_code}")
print(f"Username: {response.json()['data']['username']}")

print("\n5. Update User")
update_data = {"age": 26}
response = requests.put(f"{BASE_URL}/api/users/{user_id}", headers=headers, json=update_data)
print(f"Status: {response.status_code}")
print(f"Updated Age: {response.json()['data']['age']}")

print("\n6. Delete User")
response = requests.delete(f"{BASE_URL}/api/users/{user_id}", headers={"X-API-Key": API_KEY})
print(f"Status: {response.status_code}")
print(f"Deleted: {response.json()['message']}")

print("\n✅ All tests completed!")
```

Сохраните как `test_api.py` и запустите: `python test_api.py`

---

**Успешного тестирования! 🎯**

