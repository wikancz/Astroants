# Astroants

Implementace Quadient soutěže [Astroant](http://quadientevents.cz/assets/quadient-soutez.pdf) v .NET Core 2.0.

* Hledání cesty je založeno na [A* algoritmu](https://en.wikipedia.org/wiki/A*_search_algorithm)
* Za samotné hledání je zodpovědná metoda **Solve()** třídy **Solver** v namespace **Astroants.Core**
* Klient pro testovací API je v namespace **Astroants.RestApiClient**
* API je volané v konzolové aplikaci v namespace **Astroans.Runner**
* Volání API v DEBUG ukládá komunikaci se serverem do souborů **planet-{PlanetID}/*.json**
    * **planet.json** obsahuje zadání úlohy ze serveru
    * **solution.json** obsahuje řešení odesílané na server
    * **result.json** obsahuje vyhodocení poslaného řešení
* Některé z těchnto souborů pak byly použity v testech v namespace **Astroants.Test**


