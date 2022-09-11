$(document).ready(function () {
    GetPeople();
    getStats();
})

function GetPeople() {
    ur = 'https://localhost:7289/api/people/all'
    $.ajax({
        url: ur,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);
            $("#peopleList").empty();
            showPeople(data);
        },
        error: function (error) {
            if (error) {
                console.log(error);
            }
        }
    });
}

function searchLastName() {
    let lastName = document.getElementById("searchbar").value
    if (lastName == "") {
        GetPeople();
        return
    }
    ur = 'https://localhost:7289/api/people/search'
    $.ajax({
        url: ur,
        type: 'GET',
        dataType: 'json',
        data: {
            "lastName": lastName
        },
        success: function (data) {
            console.log(data);
            $("#peopleList").empty();
            showPeople(data);
        },
        error: function (error) {
            if (error) {
                console.log(error);
            }
        }
    });
}

function showPeople(data) {
    data.forEach(data => {

        $('#peopleList').append(
            `<tr>
			<td id="first${data.id}">${data.firstName}</td>
			<td id="last${data.id}">${data.lastName}</td>
			<td id="age${data.id}">${data.age}</td>
			<td id="gender${data.id}">${data.gender}</td>
			<td id="lon${data.id}">${data.longitude}</td>
			<td id="lat${data.id}">${data.latitude}</td>
			<td id="alive${data.id}">${data.alive}</td>
            <td><button id="${data.id}" onclick="editPerson(this.id)">Edit</button></td>
            </tr>`
        );
    })
}

function editPerson(id) {
    console.log(id);

    let firstName = document.getElementById("first" + id).innerHTML
    let lastName = document.getElementById("last" + id).innerHTML
    let age = document.getElementById("age" + id).innerHTML
    let gender = document.getElementById("gender" + id).innerHTML
    let longitude = document.getElementById("lon" + id).innerHTML
    let latitude = document.getElementById("lat" + id).innerHTML
    let alive = document.getElementById("alive" + id).innerHTML

    document.getElementById("idE").value = id;
    document.getElementById("firstE").value = firstName;
    document.getElementById("lastE").value = lastName;
    document.getElementById("ageE").value = age;
    document.getElementById("gender").value = gender;
    document.getElementById("lonE").value = longitude;
    document.getElementById("latE").value = latitude;
    document.getElementById("status").value = alive;

    console.log("fe");
}

function showStats(data) {
 
        $('#stats').append(
            `<label id="survivors">Survivors: ${data.survivors}</label>
                    <br />
                    <label id="deceased">Deceased: ${data.deceased}</label>
                    <br />
                    <label id="pctAlive">Percent alive: ${data.pctageOfsurvivors}%</label>
                    <br />
                    <label id="pctNorth">Percent on northern hemisphere: ${data.pctOnNorthHem}%</label>
                    <br />
                    <label id="pctSouth">Percent on southern hemisphere: ${data.pctONSouthHem}%</label>`
        );
    }



function getStats() {
    ur = 'https://localhost:7289/api/people/stats'
    $.ajax({
        url: ur,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            showStats(data);
        },
        error: function (error) {
            if (error) {
                console.log(error);
            }
        }
    });
}

function createPerson() {



}
