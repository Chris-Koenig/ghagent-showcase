import { useState, useEffect } from 'react';
import type { User, CreateUserDto } from '../types/user';
import { userService, ApiError } from '../services/api';
import UserList from '../components/UserList';
import UserForm from '../components/UserForm';
import './Users.css';

const Users: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [selectedUser, setSelectedUser] = useState<User | null>(null);

  useEffect(() => {
    loadUsers();
  }, []);

  const loadUsers = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await userService.getUsers();
      setUsers(data);
    } catch (err) {
      if (err instanceof ApiError) {
        setError(`Failed to load users: ${err.message}`);
      } else {
        setError('An unexpected error occurred');
      }
    } finally {
      setLoading(false);
    }
  };

  const handleCreateUser = async (userDto: CreateUserDto) => {
    try {
      setError(null);
      const newUser = await userService.createUser(userDto);
      setUsers([...users, newUser]);
    } catch (err) {
      if (err instanceof ApiError) {
        setError(`Failed to create user: ${err.message}`);
      } else {
        setError('An unexpected error occurred');
      }
    }
  };

  const handleDeleteUser = async (id: number) => {
    if (!window.confirm('Are you sure you want to delete this user?')) {
      return;
    }

    try {
      setError(null);
      await userService.deleteUser(id);
      setUsers(users.filter((u) => u.id !== id));
      if (selectedUser?.id === id) {
        setSelectedUser(null);
      }
    } catch (err) {
      if (err instanceof ApiError) {
        setError(`Failed to delete user: ${err.message}`);
      } else {
        setError('An unexpected error occurred');
      }
    }
  };

  const handleUserSelect = (user: User) => {
    setSelectedUser(user);
  };

  return (
    <div className="users-page">
      <header className="page-header">
        <h1>User Management</h1>
        <p>Manage your users efficiently and securely</p>
      </header>

      {error && (
        <div className="error-banner">
          <span>⚠️ {error}</span>
          <button onClick={() => setError(null)}>✕</button>
        </div>
      )}

      <div className="page-content">
        <section className="form-section">
          <h2>{selectedUser ? 'Edit User' : 'Create New User'}</h2>
          <UserForm
            user={selectedUser || undefined}
            onSubmit={handleCreateUser}
            onCancel={selectedUser ? () => setSelectedUser(null) : undefined}
          />
        </section>

        <section className="list-section">
          <h2>Users ({users.length})</h2>
          <UserList
            users={users}
            loading={loading}
            onUserSelect={handleUserSelect}
            onUserDelete={handleDeleteUser}
          />
        </section>
      </div>
    </div>
  );
};

export default Users;
