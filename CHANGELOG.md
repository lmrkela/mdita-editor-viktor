## 1.2.1

#### Dodaci
- Exception forma
- Dodat jezik Ruby u Snippet kontrolu
- Dodata lista prethodno otvaranih projekata u glavnom meniju

#### Izmene
- Deaktivirane opcije u meniju ukoliko projekat nije učitan
- Snippet forma urađena da lepše prikazuje programski kod
- Omogućeno učitavanje koda iz fajla direktno u Snippet edit formi

#### Ispravljeni bagovi
- U Q&A formi ispravljeno dugme za brisanje pitanja
- Ispravljena boja naslova u Forum formi


## 1.2.0

#### Dodaci
- Preview HTML-a unutar programa
- Nova forma za update programa
- Offline mod za LaTeX
- Forma za prijavu greške unutar programa

#### Izmene
- Dorada formi za Dodatne aktivnosti
- Forma za update programa prikazuje changelog i verziju
- Preview HTML forma prikazuje progres

#### Ispravljeni bagovi
- Prikazivanje LaTeX-a
- Generacija HTML-a u nekim slučajevima nije radila kako treba
- Ispravljena JavaGrader forma
- Ispravljene greške u objektima koje su onemogućavale import u LAMS
- Ispravljen bag kod Q&A forme
- Ispravljen bag kod korišćenja Term i Keyword text formata


## STARE VERZIJE

#### v1.1.4
- Popravljen brojac reci za neke importovane TextBoxove
- Popravljen brojac objekata kao i testova posle objekata u procentima
- Popravljen bug cuvanja XML-a kroz Note koji sadrzi <pre npr <predmet> <prezime>
- Popravljen Note link koji je pravio problem


#### v0.95
- Popravljenja mogućnost kopiranja i nalepljivanja u TextBox sa opcijom desni klik Paste
- Dodata opcija Undo i Redo za TextBox na desni klik
- Uvedena mogućnost kreiranja numeričke lista i bullet liste bez selektovanja teksta
- Dodata mogućnost pretvaranja objekat u podobjekat drugog objekta
- Sređen bug pri uvozu Java koda kroz Insert Snippet by Attachment opciju
- Uvedene mogućnosti za pisanje tangensa kao tg, kotangensa kao ctg, arkus tangensa kao arctg i arkus kotangensa kao arcctg u LaTeX-u.
- Uvedena opcija \gbreak u LaTeX-u koja omogućuje preporučeno prelamanje formula
- Izmenjena je putanja za CDN na mDiti koji služi za prikazivanje lepo formatiranog koda. (Staru je Google ukinuo)
- mDita Editor sada pored zadnjih šifara predmeta pamti i poslednjeg unetog autra kako bi ste izbegli pisanje istog više puta


#### v0.90
- Dodata Drag n Drop funkcija za dodavanje slika u lekcije
- Dodata mogućnost pravljenja opcionih aktivnosti u LAMS sekvenci
- Dodata mogućnost pravljenja kapije aktivnosti u LAMS sekvenci
- Ispravljen bug prebacivanja vrsta kolona
- Ispravljeni neki specijalni karakteri koji su se pojavljivali
- Dodata mogućnost paste na desni klik u TextBox kao i kopiranja i isecanja
teksta na desni klik unutar teksta unutar TextBox-a i Note-a.
- Dodat HTML Preview - Na File pa HTML Preview sada možete videti kako će 
Vaša lekcija izgledati bez da je šaljete na obradu (HTML verziju lekcije)
- Dodata je LAMS aktivnost Java grader koja je takođe dodata i u LAMS-u
- Rešeni problemi vezani za Forum kao dodatnu aktivnost
- Dodata mogućnost skrolovanja sadržaja ukoliko na neki način pređe veličina
kolone
- Popravljeno čuvanje DITA XML-a pri čuvanju teksta koji ne počinje paragrafom
- Sređen bug pri Undo funkciji nad promenom vrste sekcija


#### Version 0.85
- Dodata opcija za menjanje pozadine i boje teksta u Note-u
- Sada se Audio i Video kace na mDita Server kako se nebi slali preko mejla
preveliki zipovi.
- Rešeni su neki bagovi prilikom čuvanja sekvence
- Popravljeni manji bagovi u Audio formi
- Dodata opcija brisanja na delete dugme kada je fokus u meniju
- Poboljšana opcija kretanja kroz listu slajdova.
- Sređeni su neki bagovi pri učitavanju starih lekcija
- Rešen bug selekcije razmaka na dupli klik miša u TextBox-u
- Rešen bug duplog prikazivanja selektovane sekcije u meniju 
- Dugme za centriranje je dodato u LAMS dizajneru
- Dupli klik u TextBox-u više ne selektuje razmak
- Dodato je prepoznavanje da li korisnik ima ili nema mikrofon


#### Version 0.80
- Editor sada pamti predmete za koje profesor pravi materijale
- Otklonjeni su bagovi sa čuvanjem LAMS sekvence
- Otklonjene su greške čuvanja ul i li HTML taga u okviru TextBox-a koji su se 
pojavljivali isključivo u lekcijama koje sadrže velik broj HTML-a van Snippeta.
- Promenjen je način učitavanja sekvenci na mDita serverskom delu.
- Promenjene su neke poruke editora da bi bile jasnije.


#### Version 0.7
- Note se sada čuva pravilno
- Čuvanje uvoda sada funkcioniše svaki put
- Dozvoljeni su specijalni karakteri: ½, ¾, ¼, ¾, —
- Rešen je problem pojavljivanja taga u textbox-u
- Ugrađen je spell check pri kucanju
- Uvedena je mogućnost prebacivanja podobjekta u objekat kroz meni na desnom kliku
- Projekat se može sačuvati čak iako ima greške (Draft verzija)
- Na desni klik na tekst slike moguće je promeniti početni tekst (Slika, Primer, Tabela).


#### Version 0.5
- Solved putting basic level on open on selected slide
- Added auto opening of last folder
- Added new Folder browser dialog with Path selection
- Extreme bold italic open tags test
- Validation on empty text boxes
- Change of menu drag n drop
- Added Share resources LAMS control
- Line Height change of TextBox
- Advanced Search
- Fix f10.5 in importing old lessons


#### Version 0.4
- Add picture on overview and summary
- Add Multiple Chooice control
- Exceptions fix
- Small letters in Section title
- Add Submit Files control
- New error list
- Special characters Fix on textbox


####Version 0.3
- Drag n drop menu
- Gallery control
- Undo states for drag n drop
- Save on exit 
- Fixes on opening existing projects
- Auto zip of project
- Fixes on LearningOverview
- Drag n drop Controls
- Fix picture add special characters
- Fix image resize on slide open
- Forum activity
- Q/A activity
- System for adding activities
- Section load faster


#### Version 0.2
- Video control
- Audio control
- YouTube control
- LaTeX editor
- Undo feature
- Fixed issues
- Faster menu recreate
- Change section images
- Fix TextBox control
- Search object online
- LaTeX add new row (br) tag
- Insert object online edit
- Auto add section on object create
- Auto add learning overview and learning summery seciton
- Edit project data