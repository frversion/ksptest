import axios from 'axios';

const API_URL = 'http://localhost:5242/api/usuario';

const login = async (email, password) => {
    const response = await axios.post(`${API_URL}/login`, { email, password });
    return response.data.token;
};

export default { login };
