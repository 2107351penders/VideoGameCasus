# Video Game casus

# TODO
- [ ] Veiligere cookies: Momenteel wordt er een normale cookies weggeschreven die de inlognaam van de user bevat. Deze cookie is makkelijk na te maken om zo toegang te krijgen. Idee: cookie met willekeurige ID uitgeven en deze ID bijhouden in database. Wanneer geauthenticeerd wordt check of deze ID overeenkomt; zo ja login en geef een nieuwe cookie met nieuw ID uit.
- [ ] Gehashte wachtwoorden: Momenteel is het wachtwoord in het datamodel en in de database plaintext i.p.v. de gehashte vorm die we voor ogen hadden (hier is functionaliteit voor aanwezig in ASP.NET)