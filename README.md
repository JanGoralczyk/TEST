# KSEF 2.0 Client

Projekt Windows Forms Application (WFA) w C# dla Visual Studio 2022 do nawiązania sesji z KSEF 2.0 (Krajowy System e-Faktur).

## Struktura projektu

- `WinFormsApp/` - Aplikacja Windows Forms dla VS 2022
- `ConsoleApp/` - Wersja konsolowa do testowania (kompatybilna z Linux/macOS)

## Funkcjonalności

- Nawiązywanie sesji z systemem KSEF 2.0
- Sprawdzanie statusu sesji  
- Terminowanie sesji
- Obsługa błędów i komunikatów

## Aplikacja Windows Forms

### Wymagania
- Visual Studio 2022
- .NET 8.0 z obsługą Windows
- System Windows

### Uruchomienie
```bash
cd WinFormsApp
dotnet build
dotnet run
```

### Funkcjonalności UI
- Pola do wprowadzenia identyfikatora i hasła
- Przyciski: Connect, Check Status, Disconnect
- Obszar wyjściowy z logami operacji

## Aplikacja konsolowa (demo/test)

### Uruchomienie
```bash
cd ConsoleApp  
dotnet build
dotnet run
```

## Implementacja KSEF 2.0

Projekt zawiera klasę `KSEFService` implementującą:

- `InitializeSessionAsync()` - Nawiązywanie sesji
- `GetSessionStatusAsync()` - Sprawdzanie statusu  
- `TerminateSessionAsync()` - Kończenie sesji

### Uwagi implementacyjne

- Aktualny kod używa podstawowego HTTP API
- Rzeczywista implementacja KSEF 2.0 może wymagać:
  - Uwierzytelniania certyfikatowego
  - Specyficznych nagłówków HTTP
  - Obsługi tokenów JWT
  - Szyfrowania danych

## Konfiguracja

Podstawowy URL KSEF: `https://ksef.mf.gov.pl/api`

## Zależności

- Newtonsoft.Json - obsługa JSON
- System.Net.Http - komunikacja HTTP
- Windows Forms (tylko dla wersji WFA)