import axios from 'axios';

const API_URL = 'http://localhost:5242/api/usuario';

const getUsers = async (token) => {
    const response = await axios.get(API_URL, {
        headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
};

const addUser = async (user, token) => {
    const response = await axios.post(API_URL, user, {
        headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
};

const editUser = async (userId, user, token) => {
    const response = await axios.put(`${API_URL}/${userId}`, user, {
        headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
};

const deleteUser = async (userId, token) => {
    const response = await axios.delete(`${API_URL}/${userId}`, {
        headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
};

export default { getUsers, addUser, editUser, deleteUser };
