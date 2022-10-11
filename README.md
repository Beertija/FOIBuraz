# FOIBuraz
Ovo je konzolno rješenja za projekt FOI Buraz. Svrha aplikacije jest ubrzati proces slanja pojedinačnih mailova u obliku parova (Mali buraz + Veliki buraz).
Također, cilj projekta jest smanjiti spam na službene mailove poput referade, studentskog zbora, itd.


## Potreban softver za pokretanje 
- Visual Studio

## Setup
Kako bi se koristila aplikacija za slanje mailova, potreban je matični mail koji omogućava 2FA (two-factor-authentication). Potrebno je omogućiti prijavu sa 2FA, te zatim dodati pravo na mailu za Windows aplikaciju.

![2FA](https://user-images.githubusercontent.com/72978858/194953244-14e87676-dcda-4628-8ef3-d54bf6754b6b.png)

Zatim pod "App passwords" odabrati kao što je prikazano na slici.

![App password](https://user-images.githubusercontent.com/72978858/194953482-0d3ade3c-0e96-4004-8f49-cd824828b67f.png)

**OPREZ! Kod koji će se generirati bit će prikazan samo u tom trenutku i nikada više.**
Taj kod će vam trebati za polje "Key" u appsettings.json file-u unutar konzolne aplikacije, a u polje "Email" se upisuje puni email za koji ste malo prije generirali ključ.

Za kraj preostaje samo pokrenuti stari google forms za FOI Buraz i tu excelicu postaviti na isto mjesto u folderu projekta gdje su i testni primjeri excelica.
Ime Excel datoteke potrebno je navesti u appsettings.json file-u.

Budući da je SMTP javno dostupna konfiguracija od kompanije Google unutar jednog pokretanja aplikacija se može poslati 100 mailova, tj. može se generirati 50 parova. Ako broj prijava prelazi broj 100 potrebno je modificirati postojeću ili generirati novu excel datoteku koja bi inkorporirala one prijave koje prelaze broj 100. U slučaju da je unutar broja prijava bio veći broj prijava velikih buraza, oni neće imati dodijeljene parove no ako je broj malih buraza veći od broja prijava velikih buraza onda za one male buraze koji nemaju dodijeljen par se nanovo dodijeljuju veliki burazi od vrha prema dolje.
