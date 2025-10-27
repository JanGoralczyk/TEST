# KSEF 2.0 Integration Guide

## Rzeczywista implementacja KSEF

Aby zintegrować aplikację z rzeczywistym systemem KSEF 2.0, należy:

### 1. Uzyskanie dostępu do KSEF
- Zarejestrowanie się w systemie KSEF
- Uzyskanie certyfikatów kwalifikowanych 
- Konfiguracja środowiska testowego

### 2. Modyfikacja implementacji

#### Uwierzytelnianie certyfikatowe
```csharp
// Przykład użycia certyfikatu
var handler = new HttpClientHandler();
handler.ClientCertificates.Add(certificate);
var httpClient = new HttpClient(handler);
```

#### Właściwe endpointy API
- Testowe: `https://ksef-test.mf.gov.pl/api/`
- Produkcyjne: `https://ksef.mf.gov.pl/api/`

#### Wymagane nagłówki
```csharp
httpClient.DefaultRequestHeaders.Add("RequestId", Guid.NewGuid().ToString());
httpClient.DefaultRequestHeaders.Add("Timestamp", DateTimeOffset.UtcNow.ToString("o"));
```

### 3. Endpointy KSEF 2.0

- `POST /common/session/initToken` - inicjacja sesji
- `GET /common/session/status/{sessionToken}` - status sesji
- `DELETE /common/session/terminate/{sessionToken}` - zakończenie sesji
- `POST /common/invoice/send` - wysłanie faktury
- `GET /common/invoice/status/{referenceNumber}` - status faktury

### 4. Bezpieczeństwo
- Wszystkie komunikaty muszą być podpisane certyfikatem
- Wymagane sprawdzanie podpisów odpowiedzi
- Obsługa tokenów JWT
- Proper error handling dla kodów błędów KSEF

### 5. Testowanie
- Użyj środowiska testowego przed przejściem na produkcję
- Przetestuj wszystkie scenariusze błędów
- Sprawdź limity API

### Przydatne linki
- [Dokumentacja KSEF](https://www.gov.pl/web/kas/krajowy-system-e-faktur)
- [Specyfikacja API](https://ksef.mf.gov.pl/web/specifications)
- [Środowisko testowe](https://ksef-test.mf.gov.pl)