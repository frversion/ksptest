import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import userService from '../../services/userService';
import { useAuth } from '../../context/AuthProvider';

const EditUser = () => {
    const { id } = useParams();
    const history = useNavigate();
    const { auth } = useAuth();

    const [nombre, setNombre] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    useEffect(() => {
        const fetchUser = async () => {
            const users = await userService.getUsers(auth);
            const user = users.find(u => u.id === parseInt(id));
            if (user) {
                setNombre(user.nombre);
                setEmail(user.email);
                setPassword(user.password);
            }
        };

        fetchUser();
    }, [id, auth]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        await userService.editUser(id, { nombre, email, password }, auth);
        history('/users');
    };

    return (
        <div style={{ padding: '20px' }}>
            <h2>Edit User</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Nombre:</label>
                    <input type="text" value={nombre} onChange={(e) => setNombre(e.target.value)} required />
                </div>
                <div>
                    <label>Email:</label>
                    <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} required />
                </div>
                <div>
                    <label>Password:</label>
                    <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} required />
                </div>
                <button type="submit">Save</button>
            </form>
        </div>
    );
};

export default EditUser;
