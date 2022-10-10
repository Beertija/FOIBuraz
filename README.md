# FOIBuraz
Ovo je konzolno rješenja za projekt FOI Buraz. Svrha aplikacije jest ubrzati proces slanja pojedinačnih mailova u obliku para (Mali buraz + Veliki buraz).
Također, cilj projekta jest smanjiti spam na službene mailove poput referade, studentskog zbora, itd.


## Requrements
- Visual Studio

## Setup
Kako bi se koristila aplikacija za slanje mailova, potreban je matični mail koji omogućava 2FA (two-factor-authentication). Potrebno je omogućiti prijavu sa 2FA, te zatim dodati pravo na mailu za Windows aplikaciju.

![2FA](https://user-images.githubusercontent.com/72978858/194953244-14e87676-dcda-4628-8ef3-d54bf6754b6b.png)

Zatim pod "App passwords" odabrati kao što je prikazano na slici.

![App password](https://user-images.githubusercontent.com/72978858/194953482-0d3ade3c-0e96-4004-8f49-cd824828b67f.png)

OPREZ! Kod koji će se generirati bit će prikazan samo u tom trenutku i nikada više.
Taj kod će vam trebati za polje "Key" u appsettings.json file-u unutar konzolne aplikacije, a u polje "Email" se upisuje puni email za koji ste malo prije generirali ključ.

