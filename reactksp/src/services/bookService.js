import axios from 'axios';

const API_URL = 'http://localhost:5242/api/libro';

const getBooks = async (token) => {
    const response = await axios.get(API_URL, {
        headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
};

const addBook = async (book, token) => {
    const response = await axios.post(API_URL, book, {
        headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
};

const editBook = async (bookId, book, token) => {
    const response = await axios.put(`${API_URL}/${bookId}`, book, {
        headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
};

const deleteBook = async (bookId, token) => {
    const response = await axios.delete(`${API_URL}/${bookId}`, {
        headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
};

export default { getBooks, addBook, editBook, deleteBook };
