import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import bookService from '../../services/bookService';
import { useAuth } from '../../context/AuthProvider';

const EditBook = () => {
    const { id } = useParams();
    const history = useNavigate();
    const { auth } = useAuth();

    const [titulo, setTitulo] = useState('');
    const [autor, setAutor] = useState('');
    const [copias, setCopias] = useState('');

    useEffect(() => {
        const fetchBook = async () => {
            const books = await bookService.getBooks(auth);
            const book = books.find(b => b.id === parseInt(id));
            if (book) {
                setTitulo(book.titulo);
                setAutor(book.autor);
                setCopias(book.copias);
            }
        };

        fetchBook();
    }, [id, auth]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        await bookService.editBook(id, { titulo, autor, copias: parseInt(copias) }, auth);
        history('/books');
    };

    return (
        <div style={{ padding: '20px' }}>
            <h2>Edit Book</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Titulo:</label>
                    <input type="text" value={titulo} onChange={(e) => setTitulo(e.target.value)} required />
                </div>
                <div>
                    <label>Autor:</label>
                    <input type="text" value={autor} onChange={(e) => setAutor(e.target.value)} required />
                </div>
                <div>
                    <label>Copias:</label>
                    <input type="number" value={copias} onChange={(e) => setCopias(e.target.value)} required />
                </div>
                <button type="submit">Save</button>
            </form>
        </div>
    );
};

export default EditBook;
