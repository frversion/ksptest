import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import bookService from '../../services/bookService';

const EditBook = () => {
    const [bookid, setId] = useState('');
    const [titulo, setTitulo] = useState('');
    const [autor, setAutor] = useState('');
    const [categoria, setCategoria] = useState('');
    const [anioPublicacion, setAnioPublicacion] = useState('');
    const [copias, setCopias] = useState('');
    const { id } = useParams();
    const navigate = useNavigate();

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
        await bookService.editBook(id, { id, titulo, autor, categoria, anioPublicacion, copias: parseInt(copias) });
        // TODO: Agregar un mensaje de exito
        navigate('/books');
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
                    <label>Categoria:</label>
                    <input type="text" value={categoria} onChange={(e) => setCategoria(e.target.value)} required />
                </div>
                <div>
                    <label>Anio de Publicacion:</label>
                    <input type="text" value={anioPublicacion} onChange={(e) => setAnioPublicacion(e.target.value)} required />
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
