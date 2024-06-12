import axios from 'axios';

const API_URL = 'http://localhost:5242/api/prestamo';

const getLoans = async (token) => {
    const response = await axios.get(API_URL, {
        headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
};

const addLoan = async (loan, token) => {
    const response = await axios.post(API_URL, loan, {
        headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
};

const returnBook = async (loanId, token) => {
    const response = await axios.put(`${API_URL}/devolver/${loanId}`, null, {
        headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
};

export default { getLoans, addLoan, returnBook };
