import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import bookService from '../../services/bookService';
import { useAuth } from '../../context/AuthProvider';

const AddBook = () => {
    console.log('entrando a listbook');
    const [titulo, setTitulo] = useState('');
    const [autor, setAutor] = useState('');
    const [copias, setCopias] = useState('');
    const [categoria, setCategoria] = useState('');
    const [aniopublicacion, setAnioPublicacion] = useState('');
    const navigate = useNavigate();
    const { auth } = useAuth();

    const handleSubmit = async (e) => {
        e.preventDefault();
        await bookService.addBook({ titulo, autor, categoria, aniopublicacion, copias: parseInt(copias) }, auth);
        navigate('/books');
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
                    <label>Categoria:</label>
                    <input type="text" value={categoria} onChange={(e) => setCategoria(e.target.value)} required />
                </div>
                <div>
                    <label>Anio de Publicacion:</label>
                    <input type="text" value={aniopublicacion} onChange={(e) => setAnioPublicacion(e.target.value)} required />
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
