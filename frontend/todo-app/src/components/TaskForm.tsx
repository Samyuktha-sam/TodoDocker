import React, {useState} from 'react';
import {TodoCreateRequest, Priority, TodoTask} from '../types/Todo';
import {useCreateTodoTask, useUpdateTodoTask} from "../services/TodoApiService.ts";

interface TaskFormProps {
    task?: TodoTask;
    isEditing?: boolean;
    onClose?: () => void;
    onSuccess?: () => void;
}

const TaskForm: React.FC<TaskFormProps> = ({ task, isEditing, onClose, onSuccess }) => {
    const [title, setTitle] = useState(task?.title || '');
    const [description, setDescription] = useState(task?.description || '');
    const [priority, setPriority] = useState<Priority>(task?.priority || Priority.LOW);

    const createMutation = useCreateTodoTask();
    const updateMutation = useUpdateTodoTask();

    const handleTitleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setTitle(e.target.value);
    };

    const handleDescriptionChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setDescription(e.target.value);
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        if (!title) {
            return;
        }

        if (isEditing && task) {
            const editedTask: TodoTask = {
                ...task,
                title,
                description,
                priority,
            };
            
            updateMutation.mutate(editedTask, {
                onSuccess: () => {
                    if (onSuccess) {
                        onSuccess();
                    }
                },
            });
        } else {
            const newTask: TodoCreateRequest = {
                title,
                description,
                priority,
            };
            
            createMutation.mutate(newTask, {
                onSuccess: () => {
                    setTitle('');
                    setDescription('');
                    if (onSuccess) {
                        onSuccess();
                    }
                },
            });
        }
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4 p-4 bg-white shadow-lg rounded-lg max-w-xl mx-auto">
            <div className="flex justify-between items-center mb-4">
                <h2 className="text-2xl font-semibold">{isEditing ? 'Edit Task' : 'Add a Task'}</h2>
                {isEditing && onClose && (
                    <button
                        type="button"
                        onClick={onClose}
                        className="text-gray-500 hover:text-gray-700"
                    >
                        ✕
                    </button>
                )}
            </div>
            <div>
                <input
                    type="text"
                    value={title}
                    onChange={handleTitleChange}
                    placeholder="Title"
                    className="mt-1 block w-full border border-gray-300 rounded-lg p-2"
                />
            </div>
            <div>
                <textarea
                    value={description}
                    onChange={handleDescriptionChange}
                    placeholder="Description"
                    className="mt-1 block w-full border border-gray-300 rounded-lg p-2 min-h-[100px]"
                />
            </div>
            <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">Priority</label>
                <div className="flex space-x-4">
                    <button
                        type="button"
                        onClick={() => setPriority(Priority.LOW)}
                        className={`flex-1 px-4 py-2 rounded-lg border transition-colors ${
                            priority === Priority.LOW
                                ? 'bg-green-100 border-green-500 text-green-700'
                                : 'border-gray-300 hover:border-green-500 hover:bg-green-50'
                        }`}
                    >
                        0 - Low
                    </button>
                    <button
                        type="button"
                        onClick={() => setPriority(Priority.MEDIUM)}
                        className={`flex-1 px-4 py-2 rounded-lg border transition-colors ${
                            priority === Priority.MEDIUM
                                ? 'bg-yellow-100 border-yellow-500 text-yellow-700'
                                : 'border-gray-300 hover:border-yellow-500 hover:bg-yellow-50'
                        }`}
                    >
                        1 - Medium
                    </button>
                    <button
                        type="button"
                        onClick={() => setPriority(Priority.HIGH)}
                        className={`flex-1 px-4 py-2 rounded-lg border transition-colors ${
                            priority === Priority.HIGH
                                ? 'bg-red-100 border-red-500 text-red-700'
                                : 'border-gray-300 hover:border-red-500 hover:bg-red-50'
                        }`}
                    >
                        2 - High
                    </button>
                </div>
            </div>
            <div>
                <div className="flex space-x-4 mt-6">
                    <button
                        type="submit"
                        className="px-6 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition-colors w-full"
                    >
                        {isEditing ? 'Save Changes' : 'Add Task'}
                    </button>
                </div>
            </div>
        </form>
    );
};

export default TaskForm;
