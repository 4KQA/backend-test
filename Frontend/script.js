var personlist = null;
$(document).ready(function() {

    // instansiates map
    map = new L.map("WorldMap", {
        center: [15, 15],
        zoom: 1.5,
        renderer: L.canvas(),
        zoomControl: false,
        attributionControl: true,
    });
    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibnV4dW8iLCJhIjoiY2t1cWxuYXh5MTNncTJvcXIydWp4azZpbCJ9.oOjUmOH9TOotzPVtpXTcAg', {
        maxZoom: 256,
        id: 'mapbox/streets-v11',
        tileSize: 512,
        id: 'mapbox/dark-v10',
        zoomOffset: -1
    }).addTo(map);

    const settings = {
        "async": true,
        "url": "https://localhost:7172/api/Survivor/stats",
        "method": "GET",
    };

    $.ajax(settings).done(function (response) {
        console.log(response);
        html = "";

        html += "<p class='BlockStyle'>Population : "+response.ammount_Survivors+"</p>"
        html += "<p class='BlockStyle'>Alive : "+response.alive_Survivors+"</p>"
        html += "<p class='BlockStyle'>Dead : "+response.dead_Survivors+"</p>"
        html += "<p class='BlockStyle'>Precentage Alive : "+response.precentage_Alive_Survivors+"%</p>"

        
        document.getElementById("stats").innerHTML = html;
    });

    const survivors = {
        "async": true,
        "url": "https://localhost:7172/api/Survivor",
        "method": "GET",
    };

    $.ajax(survivors).done(function (response) {
        console.log(response);
        html = "";

        personlist = response;
        response.forEach(element => {
            html += "<option value="+element.survivor_ID+">"+element.firstName+"</option>"
            var marker = L.marker([element.latitude, element.longitude]).addTo(map);
            if(element.alive == false){
                marker._icon.classList.add("huechange");    
            }
        });

        document.getElementById("survivorsList").innerHTML = html;

    });
});

function changePerson(){
    var index = document.getElementById("survivorsList").value - 1;
    document.getElementsByName("firstname")[0].value = personlist[index].firstName;
    document.getElementsByName("lastname")[0].value = personlist[index].lastName;
    document.getElementsByName("lat")[0].value = personlist[index].latitude;
    document.getElementsByName("lng")[0].value = personlist[index].longitude;
    document.getElementsByName("alive")[0].checked = personlist[index].alive;
};

function updateSurvior(){
    var index = document.getElementById("survivorsList").value;

    // const data = {
    //     "firstName": document.getElementsByName("firstname")[0].value,
    //     "lastName": document.getElementsByName("lastname")[0].value,
    //     "longitude": document.getElementsByName("lat")[0].value,
    //     "latitude": document.getElementsByName("lng")[0].value,
    //     "alive": document.getElementsByName("alive")[0].checked,
    // }

    const data = {
        "lastName": "tester",
        "age": 25,
        "gender": "Female",
        "longitude": 60,
        "latitude": -51,
        "alive": true
      }

    console.log(data);
    const survivors = {
        "async": true,
        "url": "https://localhost:7172/api/Survivor/"+index,
        "method": "PUT",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };

    $.ajax(survivors).done(function (response) {
        document.getElementById("respond").value = response;
    });
};


