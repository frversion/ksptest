import axios from 'axios';

const API_URL = 'http://localhost:5242/api/usuario';
const config = {
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    }
}

const login = async (email, password) => {
    const response = await axios.post(`${API_URL}/login`, {email, password }, config);
    return response.data.token;
};

export default { login };
