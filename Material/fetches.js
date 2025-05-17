let CetusData;
function Cetus() {
    fetch("https://api.warframestat.us/pc/cetusCycle")
        .then(response => response.json())
        .then(data => {
            CetusData = data;
            document.querySelector("#cetStatus").textContent = "Estado: " + CetusData.state;
            document.querySelector("#cetTime").textContent = "Tiempo restante: " + CetusData.timeLeft;
        })
        .catch(error => console.error('Error al cargar el archivo JSON:', error));
}

let FortunaData;
function Fortuna() {
    fetch("https://api.warframestat.us/pc/vallisCycle")
        .then(response => response.json())
        .then(data => {
            FortunaData = data;
            document.querySelector("#forStatus").textContent = "Estado: " + FortunaData.state;
            document.querySelector("#forTime").textContent = "Tiempo restante: " + FortunaData.timeLeft;
        })
        .catch(error => console.error('Error al cargar el archivo JSON:', error));
}

let DerivaData;
function Deriva() {
    fetch("https://api.warframestat.us/pc/cambionCycle")
        .then(response => response.json())
        .then(data => {
            DerivaData = data;
            document.querySelector("#derStatus").textContent = "Estado: " + DerivaData.state;
            document.querySelector("#derTime").textContent = "Tiempo restante: " + DerivaData.timeLeft;
        })
        .catch(error => console.error('Error al cargar el archivo JSON:', error));
}

let NightwaveData;
function Nightwave() {
    fetch("https://api.warframestat.us/pc/nightwave")
        .then(response => response.json())
        .then(data => {
            NightwaveData = data;
            document.querySelector("#nwStatus").textContent = "Estado: " + (NightwaveData.active ? "Activo" : "Inactivo");
            if (NightwaveData.activeChallenges.length > 0) {
                document.querySelector("#nwTime").textContent = "Tiempo restante: " + NightwaveData.activeChallenges[0].eta; // Muestra el tiempo del primer desafío activo
            } else {
                document.querySelector("#nwTime").textContent = "No hay desafíos activos.";
            }
        })
        .catch(error => console.error('Error al cargar el archivo JSON:', error));
}

let FissureData;
function Fissures() {
    fetch("https://api.warframestat.us/pc/fissures")
        .then(response => response.json())
        .then(data => {
            FissureData = data;
            let fissureList = "";
            FissureData.forEach(fissure => {
                fissureList += `${fissure.missionType} (${fissure.tier}) - Tiempo restante: ${fissure.eta}<br>`;
            });
            document.querySelector("#fissureList").innerHTML = fissureList;
        })
        .catch(error => console.error('Error al cargar el archivo JSON:', error));
}

let ArbitrationData;
function Arbitration() {
    fetch("https://api.warframestat.us/pc/arbitration")
        .then(response => response.json())
        .then(data => {
            ArbitrationData = data;
            document.querySelector("#arbMission").textContent = "Misión: " + ArbitrationData.type;
            document.querySelector("#arbEnemy").textContent = "Enemigos: " + ArbitrationData.enemy;
        })
        .catch(error => console.error('Error al cargar el archivo JSON:', error));
}

let SortieData;
function Sortie() {
    fetch("https://api.warframestat.us/pc/sortie")
        .then(response => response.json())
        .then(data => {
            SortieData = data;
            let sortieList = "";
            SortieData.variants.forEach(sortie => {
                sortieList += `${sortie.missionType} en ${sortie.node} - Modificador: ${sortie.modifier}<br>`;
            });
            document.querySelector("#sortieList").innerHTML = sortieList;
            document.querySelector("#sortieBoss").textContent = "Jefe: " + SortieData.boss;
            document.querySelector("#sortieTime").textContent = "Tiempo restante: " + SortieData.eta;
        })
        .catch(error => console.error('Error al cargar el archivo JSON:', error));
}
