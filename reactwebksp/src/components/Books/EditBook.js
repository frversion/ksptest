import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import bookService from '../../services/bookService';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { Alert } from 'react-bootstrap';

const EditBook = () => {
    const navigate = useNavigate();
    const auth = localStorage.getItem("token");
    if (!auth) {
        navigate("/login");
    } 
    const [bookid, setId] = useState('');
    const [titulo, setTitulo] = useState('');
    const [autor, setAutor] = useState('');
    const [categoria, setCategoria] = useState('');
    const [anioPublicacion, setAnioPublicacion] = useState('');
    const [copias, setCopias] = useState('');
    const { id } = useParams();

    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');


    useEffect(() => {
        const fetchBook = async () => {
            const books = await bookService.getBooks();
            const currentBook = books.find(b => b.id === parseInt(id));
            if (currentBook) {
                setId(currentBook.id);
                setTitulo(currentBook.titulo);
                setAutor(currentBook.autor);
                setCategoria(currentBook.categoria);
                setAnioPublicacion(currentBook.anioPublicacion);
                setCopias(currentBook.copias);
            }
        };

        fetchBook();
    }, [id]);

    const handleSubmit = async (e) => {
        e.preventDefault();

        setError('');
        setSuccess('');

        if (!titulo || !autor || !copias || !categoria || !anioPublicacion) {
            setError('Todos los campos son obligatorios.');
            return;
        }

        if (isNaN(anioPublicacion) || anioPublicacion < 1500 || anioPublicacion > 2024) {
            setError('El campo "Anio publicacion" debe ser un numero entero positivo entre 1500 y 2024.');
            return;
        }

        if (isNaN(copias) || copias < 0) {
            setError('El campo "copias" debe ser un numero entero positivo igual o mayor a cero.');
            return;
        }

        try {
            const response = await bookService.editBook(id, { id, titulo, autor, categoria, anioPublicacion, copias: parseInt(copias) }, auth);

            if (response.isSuccess) {
                setSuccess(response.resultMessage);
                setTimeout(() => {
                    navigate('/BookList'); // Redirect to the book list
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

    const handleCancel = () => {
        navigate('/BookList');
    };

    return (
        <div>
            {error && <Alert variant="danger">{error}</Alert>}
            {success && <Alert variant="success">{success}</Alert>}
        <form onSubmit={handleSubmit}>
            <Form>
                <Form.Group className="mb-3" controlId="FormAddBook.ControlInput1">
                    <Form.Label>Titulo</Form.Label>
                    <Form.Control type="text" value={titulo} onChange={(e) => setTitulo(e.target.value)} required autoFocus />
                    <Form.Label>Autor</Form.Label>
                    <Form.Control type="text" value={autor} onChange={(e) => setAutor(e.target.value)} required />
                    <Form.Label>Categor&iacute;a</Form.Label>
                    <Form.Control type="text" value={categoria} onChange={(e) => setCategoria(e.target.value)} required />
                    <Form.Label>A&ntilde;o Publicacion</Form.Label>
                    <Form.Control type="text" value={anioPublicacion} onChange={(e) => setAnioPublicacion(e.target.value)} required />
                    <Form.Label>Copias</Form.Label>
                    <Form.Control type="text" value={copias} onChange={(e) => setCopias(e.target.value)} required />
                </Form.Group>
            </Form>
            <Button variant="primary" type="button" onClick={handleCancel}>Cancelar</Button>
                &nbsp;
            <Button variant="primary" type="submit">Guardar</Button>
        </form>
        </div>
    );
};

export default EditBook;
