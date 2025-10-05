import React from 'react';
import TodoItem from './TodoItem';
import {useGetTodoTasks} from "../services/TodoApiService.ts";
import { TodoTask } from '../types/Todo.ts';

const TodoList: React.FC = () => {
  const { data: todos, isLoading, isError } = useGetTodoTasks();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error loading todos</div>;

  // Filter incomplete tasks and get the 5 most recent ones
  const activeTodos = todos
    ?.filter((todo: TodoTask) => !todo.isCompleted)
    .sort((a: TodoTask, b: TodoTask) => 
      new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
    )
    .slice(0, 5);

  return (
    <div className="space-y-4 p-4">
      {activeTodos && activeTodos.length > 0 ? (
          activeTodos.map((todo: TodoTask) => (
              <TodoItem key={todo.id} todo={todo} />
          ))) : (
        <p className="text-gray-500">No active tasks</p>
      )}
    </div>
  );
};

export default TodoList;
