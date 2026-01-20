# BitcoinApp

## Popis projektu
BitcoinApp je webová aplikace postavená na ASP.NET Core 10.0, která poskytuje informace o cenách Bitcoinu a umožňuje uživatelům sledovat aktuální cenu a ukládat si konkrétní záznamy.

### Server
Serverová část aplikace využívá Entity Framework Core pro práci s databází SQL Server a poskytuje REST API pro komunikaci s klientskou částí. Periodicky stahuje informace o ceně bitcoinu, které si drží v paměti. Jednou za den také stáhne aktuální kurz přes rozhraní KB, pomocí kterého převede cenu z EUR na CZK.
Databázové řešení je realizováno pomocí Code First přístupu, správa databáze je zajištěna přes migrace. K přístupu k datům je použit Repository Pattern, jinak se snažím mít logiku v servisních třídách a kontrolery slouží hlavně pro komunikaci.

### Klient
Klientská část je vytvořena také pomocí ASP.NET Core 10.0 MVC, generuje stránky popřes Views přes klasický Razor a komunikuje se serverovou částí přes REST API.

Část "Live Data" se periodicky aktualizuje pomocí JavaScriptu a AJAX volání, které získávají aktuální "View" ze serveru bez nutnosti obnovovat celou stránku.
Po kliknutí na tlačítko "Uložit data" se provede AJAX volání na server, který daný záznam uloží do databáze. Uložené záznamy dostanou světle zelené podbarvení.

Část "Saved Data" používá javaskriptovou knihovnu "Toust UI Grid" (https://ui.toast.com/tui-grid) pro správu záznamů (řešení sortování, filtrování, úpravy a validací).
Pomocí checkboxů lze záznamy označit a následně je smazat tlačítkem "Smazat vybrané záznamy". Dále lze editovat Poznámky přímo v tabulce. Po editaci se provede validace na straně klienta (červené podbarvení). Upravené záznamy se uloží do databáze po kliknutí na tlačítko "Uložit změny".

Pro potřeby popisu a testování rozhraní je k dispozici swagger (../swagger/index.html).

## Návod ke spuštění aplikace

### Server

Příprava databáze
- Vytvoříme prázdnou databázi na SQL serveru.
- Upravíme connection string v appsettings.json tak, aby ukazoval na nově vytvořenou databázi.
- Spustíme migrace v Package Manager Console/PowerShell: update-database

### Klient
- Upravíme hodnotu BitcoinApiSettings.ApiUrl v appsettings.json tak, aby ukazoval na serverovou část aplikace (pokud je potřeba).

### Spuštění aplikace
Nejjednodušeji např. přes Visual Studio - Otevřeme solution a spustíme jako Multiple startup projects (Server i Client).

## Poznámky
- Není to moc odladěné (testoval jsem jen na Chrome), jde jen o ukázkový projekt.
- Nestihl jsem omezit počet záznamů v paměti, takže při dlouhodobém běhu může dojít k vyčerpání paměti. Vzhledem k tomu, že jde o ukázkový projekt, jsem nepředpokládal reálné využití, ale jen krátkodobé spuštění.
- Nastavení periodicity stahování dat a obnovování Live Data je možné upravit v appsettings.json (pro testovací účely jsem nastavil 30s).
- Nestihl jsem přidat Grafy pro vizualizaci dat - čistě ze zájmu si to pak ještě dodělám.
- Kód je místy nesourodý a zaslouží si refaktoring. Ale také ukazuje více přístupů k řešení.
- Sem tam mi chybí interface, jinde zase přebývá. Konkrétně v tomto případě by tam ostatně nemusel být žádný.
- Pokud jde knihovnu pro Grid, tak jsem zvolil Toust UI, protože je zdarma pro komerční použití (léta jsem teď pracoval s Telerikem, který je ale přes licenci). Chvíli mi trvalo, než jsem se s ní trochu zžil, proto to zpoždění. I tak jsem nestihl úplně odladit design a chování gridu (vybírání needitovatelných buněk apod).

### Known bugs
- Nepodařilo se mi rychle přijít na to, jak v Toust UI Grid zabránit implicitnímu konfirmačnímu dialogu před voláním Api (uložit změny, smazat záznamy). Při mazání tak může dojít k tomu, že pokud konfirmaci nepotvrdím, reálné záznamy se sice nesmažou, ale ze stránky ano. Je tak potřeba ji znovu načíst.