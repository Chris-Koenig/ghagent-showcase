import type { User } from '../types/user';
import './UserList.css';

interface UserListProps {
  users: User[];
  onUserSelect: (user: User) => void;
  onUserDelete: (id: number) => void;
  loading?: boolean;
}

const UserList: React.FC<UserListProps> = ({ 
  users, 
  onUserSelect, 
  onUserDelete, 
  loading = false 
}) => {
  if (loading) {
    return <div className="loading">Loading users...</div>;
  }

  if (users.length === 0) {
    return <div className="empty-state">No users found. Create your first user!</div>;
  }

  return (
    <div className="user-list">
      {users.map((user) => (
        <div key={user.id} className="user-item">
          <div className="user-info" onClick={() => onUserSelect(user)}>
            <div className="user-name">{user.name}</div>
            <div className="user-email">{user.email}</div>
          </div>
          <button 
            className="delete-btn" 
            onClick={() => onUserDelete(user.id)}
            aria-label={`Delete ${user.name}`}
          >
            Delete
          </button>
        </div>
      ))}
    </div>
  );
};

export default UserList;
