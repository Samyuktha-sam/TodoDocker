import React, { useState } from "react";
import { FaPencilAlt, FaTrash } from 'react-icons/fa';
import {TodoItemProps, Priority} from '../types/Todo';
import {useCompleteTodoTask, useDeleteTodoTask} from "../services/TodoApiService.ts";
import TaskForm from "./TaskForm.tsx";

const TodoItem: React.FC<TodoItemProps> = ({todo}) => {
    const [isEditing, setIsEditing] = useState(false);
    const completeMutation = useCompleteTodoTask();
    const deleteMutation = useDeleteTodoTask();

    const handleComplete = () => {
        completeMutation.mutate(todo.id);
    };

    const handleDelete = () => {
        if (window.confirm("Are you sure you want to delete this task?")) {
            deleteMutation.mutate(todo.id);
        }
    };

    return (
        <>
            <div className="bg-gray-100 rounded-lg p-4 mb-3 group">
                <div className="flex flex-col">
                    <div className="flex justify-between items-start mb-2">
                        <div className="flex-grow">
                            <div className="flex items-center gap-2 mb-1">
                                <h3 className="text-lg font-medium">{todo.title}</h3>
                                <span className={`text-xs px-2 py-1 rounded-full ${
                                    todo.priority === Priority.LOW 
                                        ? 'bg-green-100 text-green-800'
                                        : todo.priority === Priority.MEDIUM
                                            ? 'bg-yellow-100 text-yellow-800'
                                            : 'bg-red-100 text-red-800'
                                }`}>
                                    {todo.priority === Priority.LOW 
                                        ? 'Low'
                                        : todo.priority === Priority.MEDIUM
                                            ? 'Medium'
                                            : 'High'}
                                </span>
                            </div>
                            <p className="text-gray-600 text-sm">{todo.description}</p>
                        </div>
                        <div className="flex items-center space-x-2 opacity-0 group-hover:opacity-100 transition-opacity">
                            <button 
                                onClick={() => setIsEditing(true)}
                                className="p-2 text-gray-500 hover:text-blue-600 rounded-full hover:bg-blue-100 transition-colors"
                                title="Edit"
                            >
                                <FaPencilAlt size={14} />
                            </button>
                            <button 
                                onClick={handleDelete}
                                className="p-2 text-gray-500 hover:text-red-600 rounded-full hover:bg-red-100 transition-colors"
                                title="Delete"
                            >
                                <FaTrash size={14} />
                            </button>
                            <button 
                                onClick={handleComplete}
                                className="px-4 py-1 bg-gray-200 text-gray-700 rounded hover:bg-gray-300 transition-colors"
                            >
                                Done
                            </button>
                        </div>
                    </div>
                </div>
            </div>

            {isEditing && (
                <div className="fixed inset-0 flex items-center justify-center z-50 bg-black bg-opacity-50">
                    <div className="bg-white p-4 rounded-lg shadow-lg max-w-xl w-full mx-4">
                        <TaskForm 
                            task={todo} 
                            isEditing={true} 
                            onClose={() => setIsEditing(false)}
                            onSuccess={() => setIsEditing(false)}
                        />
                    </div>
                </div>
            )}
        </>
    );
};

export default TodoItem;
