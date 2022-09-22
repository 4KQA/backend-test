import React, { useState, useEffect } from 'react';
import functions from "../react/reactFunction";
function Survivors() {
    const [Survivor, setSurvivor] = useState([
        { UserFname: "", userLname: "", UserLat: "", UserLon: "", Gender: "" }
    ]);

    useEffect(() => {
        functions.fetchData("getSurvivors", updates);
    }, [functions]);

    const updates = (data) => {
        const SurvivorList = [];
        data.map((i) => {
            SurvivorList.push({
                UserFname: i.UserFname,
                UserLname: i.UserLname,
                UserLat: i.UserLat,
                UserLon: i.UserLon,
                Gender: i.Gender,
            });
        });
        setSurvivor(SurvivorList);
    };
    function tableRows() {
        return Survivor.map((i) => {
            return (
                <div>
                <tr>
                    <td>{i.UserFname}</td>
                    <td>{i.UserLname}</td>
                    <td>{i.UserLat}</td>
                    <td>{i.UserLon}</td>
                    <td>{i.Gender}</td>
                    </tr>
                    </div>
            );
        });
    }
    return (  
        <div>
        <h1>Currently recorded survivors</h1>
        <table className="table table-hover table-striped">
            <thead>
                <tr>
                    <th scope="col">ID</th>
                    <th scope="col">First name</th>
                    <th scope="col">Last name</th>
                    <th scope="col">Latitude</th>
                    <th scope="col">Longitude</th>
                    <th scope="col">Gender</th>
                </tr>
            </thead>
            <tbody>{tableRows()}</tbody>
            </table>
            
        </div>
    )
}
const getLists = Survivors()
export default getLists;