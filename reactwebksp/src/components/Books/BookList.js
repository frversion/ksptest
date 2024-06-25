import React, { useEffect, useState } from 'react';
import bookService from '../../services/bookService';
import { useNavigate } from 'react-router-dom';
//import { useAuth } from '../../context/AuthProvider';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import Alert from 'react-bootstrap/Alert';

const BookList = () => {

    const navigate = useNavigate();
    const auth = localStorage.getItem("token");

    if (!auth) {
        navigate("/login");
    } 

    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    const [books, setBooks] = useState([]);

    useEffect(() => {
        const fetchBooks = async (auth) => {
            const books = await bookService.getBooks(auth);
            setBooks(books);
        };

        fetchBooks();
    }, [auth]);

    const handleDelete = async (id, auth) => {

        setError('');
        setSuccess('');

        //alert('hello ' + auth);
        try {
            const response = await bookService.deleteBook(id, auth);

            if (response.isSuccess) {
                setSuccess(response.resultMessage);
                setTimeout(() => {
                    //navigate('/BookList'); // Redirect to the book list
                    setBooks(books.filter(book => book.id !== id));
                }, 2000);
            }
            else {
                setError(response.errorMessage || "Ocurrio un error.");
            }
        }
        catch (err) {
            setError(err.Message)
        }
    };

    return (
        <div>
            {error && <Alert variant="danger">{error}</Alert>}
            {success && <Alert variant="success">{success}</Alert>}
            <Table striped bordered hover>
            <thead>
                <tr>
                    <th align="center">#</th>
                    <th>T&iacute;tulo</th>
                    <th>Autor</th>
                    <th>Categor&iacute;a</th>
                    <th>A&ntilde;o Publicaci&oacute;n</th>
                    <th>Copias</th>
                    <th colSpan="2"></th>
                </tr>
            </thead>
            <tbody>
                {books.map((book) => (
                    <tr>
                        <td align="center"  key={book.id}>{book.id}</td>
                        <td>{book.titulo}</td>
                        <td>{book.autor}</td>
                        <td>{book.categoria}</td>
                        <td align="center">{book.anioPublicacion}</td>
                        <td align="center">{book.copias}</td>
                        <td align="center"><Button variant="primary" size="sm" href={`/EditBook/${book.id}`}>Editar</Button></td>
                        <td align="center"><Button variant="outline-danger" size="sm" onClick={() => handleDelete(book.id, auth)}>Eliminar</Button></td>
                    </tr>
                ))}
            </tbody>
        </Table>
            <Button variant="primary" href={"/AddBook/"}>Agregar libro</Button>
        </div>
    );
};

export default BookList;
