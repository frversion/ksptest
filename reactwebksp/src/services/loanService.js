import axios from 'axios';
//import setAuthToken from './Helpers/setAuthToken';

const API_URL = 'http://localhost:5242/api/prestamo';
const config = {
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    }
}

const getLoans = async () => {
    //setAuthToken(`Bearer ${token}`);
    const response = await axios.get(API_URL); //, {
    //    headers: { Authorization: `Bearer ${token}` }
    //});
    return response.data;
};

const addLoan = async (loan, token) => {
    //setAuthToken(`Bearer ${token}`);
    const response = await axios.post(API_URL, loan, config);  // {
    //    headers: { Authorization: `Bearer ${token}` }
    //});
    return response.data;
};

const returnBook = async (loanId, token) => {
    //setAuthToken(`Bearer ${token}`);
    const response = await axios.post(`${API_URL}/devolver/${loanId}`, null); //, {
    //    headers: { Authorization: `Bearer ${token}` }
    //});
    return response.data;
};

export default { getLoans, addLoan, returnBook };
