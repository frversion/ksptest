import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import bookService from '../../services/bookService';
import { useAuth } from '../../context/AuthProvider';

const AddBook = () => {
    const [titulo, setTitulo] = useState('');
    const [autor, setAutor] = useState('');
    const [copias, setCopias] = useState('');
    const { auth } = useAuth();
    const history = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        await bookService.addBook({ titulo, autor, copias: parseInt(copias) }, auth);
        history('/books');
    };

    return (
        <div style={{ padding: '20px' }}>
            <h2>Add Book</h2>
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
                <button type="submit">Add</button>
            </form>
        </div>
    );
};

export default AddBook;
