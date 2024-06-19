import axios from 'axios';
import setAuthToken from './Helpers/setAuthToken';

const API_URL = 'http://localhost:5242/api/usuario';
var config = {
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    }
}

const getUsers = async (token) => {        // = async () =>
    setAuthToken(`Bearer ${token}`);
    const response = await axios.get(API_URL, config); //, {
    //    headers: { Authorization: `Bearer ${token}` }
    //});
    return response.data;
};

const getUserById =  async (id, token) => {     // async (id) => {
    const response = await axios.get(`${API_URL}/${id}`); //, {
    //    headers: { Authorization: `Bearer ${token}` }
    //});
    return response.data;
};

const addUser = async (user, token) => { // }, token) => {
    setAuthToken(`Bearer ${token}`);
    const response = await axios.post(API_URL, user, config); //, {
    //    headers: { Authorization: `Bearer ${token}` }
    //});
    return response.data;
};

const editUser1 = async (id, user, token) => {        // async (id, user) => {
    setAuthToken(`Bearer ${token}`);
    const response = await axios.put(`${API_URL}/${id}`, user, config); //, {
        //headers: { Authorization: `Bearer ${token}` }
    //});
    return response.data;
};

const deleteUser = async (id, token) => { //async (userId, token) => {     // async (id) => {
    setAuthToken(`Bearer ${token}`);
    const response = await axios.delete(`${API_URL}/${id}`); //, {
    //    headers: { Authorization: `Bearer ${token}` }
    //});
    return response.data;
};

export default { getUsers, getUserById, addUser, editUser1, deleteUser };
