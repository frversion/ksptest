import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import userService from '../../services/userService';
import { useAuth } from '../../context/AuthProvider';

const EditUser = () => {
    const { id } = useParams();

    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    //const { auth } = useAuth();
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchUser = async () => {
            const user1 = await userService.getUserById(id);
            const users = await userService.getUsers(); // (auth);
            const user = users.find(u => u.id === parseInt(id));
            if (user) {
                setUsername(user.username);
                setEmail(user.email);
                setPassword(user.password);
            }
        };

        fetchUser();
    }, [id]); //, [id, auth]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        await userService.editUser(id, { id, username, email, password }); //, auth);
        //TODO: Agregar mensaje de exito
        navigate('/users');
    };

    if (loading) {
        return <div>Loading...</div>;
    }

    return (
        <div style={{ padding: '20px' }}>
            <h2>Edit User</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Nombre:</label>
                    <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} required />
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
