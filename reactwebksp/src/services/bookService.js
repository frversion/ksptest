import axios from 'axios';
import setAuthToken from './Helpers/setAuthToken';

const API_URL = 'http://localhost:5242/api/libro';
var config = {
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    }
}

//String.prototype.replaceInMyHeader = function (find, replace) {
//    return find.split(replace).flatMap((i) => [i, replace]).slice(0, -1);
//}

const getBooks = async () => {
    try {
        const response = await axios.get(API_URL);
        return response.data;
    } catch (error) {
        handleError(error);
    }
};

    const addBook = async (book, token) => {
        //try {

        //const parsedBook = JSON.stringify(book);
        await console.log(API_URL);
        await console.log(config);
        await console.log(book);


        //const response = await axios.post(API_URL, book, {
        //    headers: {
        //        'Accept': 'application/json',
        //        'Content-Type': 'application/json',
        //        'Authorization': `Bearer ${token}` }
        //});

        setAuthToken(`Bearer ${token}`);
        const response = await axios.post(API_URL, book, config);

        //.then((response) => {
        //    console.log(API_URL);
        //    console.log(config);
        //    console.log(book);
        //    console.log(response)
        //}) ;
        return response.data;
        //} catch (error) {
        //    handleError(error);
        //}
    
};

const editBook = async (id, book) => {
    const response = await axios.put(`${API_URL}/${id}`, book, config);
    return response.data;
};

const deleteBook = async (id) => {
    try {
        const response = await axios.delete(`${API_URL}/${id}`);
        return response.data;
    } catch (error) {
        handleError(error);
    }
};

const handleError = (error) => {
    if (error.response) {
        throw new Error(`Error ${error.response.status}: ${error.response.data}`);
    } else if (error.request) {
        throw new Error('No response from server.');
    } else {
        throw new Error('Request error: ' + error.message);
    }
};

export default {
    getBooks,
    addBook,
    editBook,
    deleteBook,
};
