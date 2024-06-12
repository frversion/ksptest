import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import bookService from '../../services/bookService';
import { useAuth } from '../../context/AuthProvider';

const BookList = () => {
    const [books, setBooks] = useState([]);
    const { auth } = useAuth();

    useEffect(() => {
        const fetchBooks = async () => {
            const books = await bookService.getBooks(auth);
            setBooks(books);
        };

        fetchBooks();
    }, [auth]);

    const handleDelete = async (id) => {
        await bookService.deleteBook(id, auth);
        setBooks(books.filter(book => book.id !== id));
    };

    return (
        <div style={{ padding: '20px' }}>
            <h2>Books</h2>
            <Link to="/add-book">Add Book</Link>
            <ul>
                {books.map((book) => (
                    <li key={book.id}>
                        {book.titulo} by {book.autor} ({book.copias} copies)
                        <Link to={`/edit-book/${book.id}`}>Edit</Link>
                        <button onClick={() => handleDelete(book.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default BookList;
