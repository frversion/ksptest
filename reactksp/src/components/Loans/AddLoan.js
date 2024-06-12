import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import loanService from '../../services/loanService';
import userService from '../../services/userService';
import bookService from '../../services/bookService';
import { useAuth } from '../../context/AuthProvider';

const AddLoan = () => {
    const [userId, setUserId] = useState('');
    const [bookId, setBookId] = useState('');
    const [users, setUsers] = useState([]);
    const [books, setBooks] = useState([]);
    const { auth } = useAuth();
    const history = useNavigate();

    useEffect(() => {
        const fetchUsersAndBooks = async () => {
            const users = await userService.getUsers(auth);
            const books = await bookService.getBooks(auth);
            setUsers(users);
            setBooks(books);
        };

        fetchUsersAndBooks();
    }, [auth]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        await loanService.addLoan({ userId, bookId }, auth);
        history('/loans');
    };

    return (
        <div style={{ padding: '20px' }}>
            <h2>Add Loan</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>User:</label>
                    <select value={userId} onChange={(e) => setUserId(e.target.value)} required>
                        <option value="">Select User</option>
                        {users.map((user) => (
                            <option key={user.id} value={user.id}>
                                {user.nombre}
                            </option>
                        ))}
                    </select>
                </div>
                <div>
                    <label>Book:</label>
                    <select value={bookId} onChange={(e) => setBookId(e.target.value)} required>
                        <option value="">Select Book</option>
                        {books.map((book) => (
                            <option key={book.id} value={book.id}>
                                {book.titulo}
                            </option>
                        ))}
                    </select>
                </div>
                <button type="submit">Add</button>
            </form>
        </div>
    );
};

export default AddLoan;
