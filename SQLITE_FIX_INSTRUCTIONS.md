# SQLite DllNotFoundException - Řešení problému

## Problém
`System.DllNotFoundException: Knihovnu DLL e_sqlite3 nelze načíst`

## Řešení

### 1. Přeinstalace NuGet balíčků (DOPORUČENO)

Otevřete **Package Manager Console** a spusťte:

```powershell
Uninstall-Package System.Data.SQLite
Uninstall-Package System.Data.SQLite.Core
Uninstall-Package System.Data.SQLite.EF6
Uninstall-Package System.Data.SQLite.Linq
Uninstall-Package Stub.System.Data.SQLite.Core.NetFramework

Install-Package System.Data.SQLite.Core -Version 1.0.118
```

### 2. Kontrola Platform Target

V **Project Properties → Build**:
- Změňte **Platform target** na: **x64** (nebo x86 podle vaší architektury)
- NEBO použijte **AnyCPU** s vypnutým **"Prefer 32-bit"**

### 3. Ověření nativních knihoven

Po rebuild zkontrolujte, že v `bin\Debug` nebo `bin\Release` existují:
- `x64\SQLite.Interop.dll`
- `x86\SQLite.Interop.dll`

### 4. Manuální kopírování (pokud automatické nefunguje)

1. Najděte NuGet balíček:
```
%USERPROFILE%\.nuget\packages\system.data.sqlite.core\1.0.118\runtimes\
```

2. Zkopírujte:
   - `win-x64\native\SQLite.Interop.dll` → `bin\Debug\x64\SQLite.Interop.dll`
   - `win-x86\native\SQLite.Interop.dll` → `bin\Debug\x86\SQLite.Interop.dll`

### 5. Rebuild projektu

```
Build → Rebuild Solution
```

## Co bylo upraveno v kódu

Metoda `SQL_OPENCONNECTION` nyní obsahuje:
- ✅ Lepší error handling s konkrétními chybovými hláškami
- ✅ Kontrolu existence databázových souborů
- ✅ Použití `SQLiteConnectionStringBuilder`
- ✅ Asynchronní otevírání připojení
- ✅ Specifickou diagnostiku DllNotFoundException

## Kontrola řešení

Po aplikaci výše uvedených kroků:
1. Restartujte Visual Studio
2. Rebuild Solution (Ctrl+Shift+B)
3. Spusťte aplikaci (F5)

Pokud problém přetrvává, zkontrolujte output konzole pro detailní chybové hlášky.
