import { useState, useEffect } from 'react';
const fetchUrl = "https://localhost:44340/endpoints/"
function reactFunctions() {
    const makeOptions = (method, body) => {
        var opts = {
            method: method,
            headers: {
                "Content-type": "application/json",
                Accept: "application/json",
            },
        };
        if (body) {
            opts.body = JSON.stringify(body);
        }
        return opts;
    };
    const fetchData = (endpoint, updateAction) => {
        const options = makeOptions("GET")
        return fetch(url + endpoint, options)
            .then(handleHttpErrors)
            .then((data) => updateAction(data))
    };
    return { fetchData, makeOptions, fetchUrl }
}
const functions = reactFunctions()
export default functions;

