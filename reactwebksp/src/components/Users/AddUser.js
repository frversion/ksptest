import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import userService from '../../services/userService';
import { useAuth } from '../../context/AuthProvider';

const AddUser = () => {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    //const { auth } = useAuth();
    const history = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        await userService.addUser({ username, email, password }); //, auth);
        history('/users');
    };

    return (
        <div style={{ padding: '20px' }}>
            <h2>Add User</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Nombre:</label>
                    <input type="username" value={username} onChange={(e) => setUsername(e.target.value)} required />
                </div>
                <div>
                    <label>Email:</label>
                    <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} required />
                </div>
                <div>
                    <label>Password:</label>
                    <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} required />
                </div>
                <button type="submit">Add</button>
            </form>
        </div>
    );
};

export default AddUser;
