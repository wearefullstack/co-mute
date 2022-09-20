$(function () {

    const authorizedEndpoint = ['/home/login', '/home/Register']
    const authorize = (element) => element.toLowerCase() === window.location.pathname.toLowerCase();
    if (!authorizedEndpoint.some(authorize))
     if(!localStorage.getItem('access_token')) window.location.replace('/home/login');

});

const headers = {
    'Content-Type': 'application/json',
    'Authorization':`Bearer ${localStorage.getItem('access_token')}`,
    'Accept': 'application/json'
}