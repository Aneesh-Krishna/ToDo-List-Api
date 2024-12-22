import React from 'react';

function TaskList({ tasks, toggleComplete, deleteTask }) {
  return (
    <ul>
      {tasks.map((task) => (
        <li
          key={task.toDoTaskId}
          style={{
            textDecoration: task.isCompleted ? 'line-through' : 'none',
          }}
        >
          <span
            onClick={() => toggleComplete(task.toDoTaskId)}
            style={{ cursor: 'pointer' }}
          >
            {task.taskName}
          </span>
          <button
            onClick={() => deleteTask(task.toDoTaskId)}
            style={{ marginLeft: '10px' }}
          >
            Delete Task
          </button>
        </li>
      ))}
    </ul>
  );
}

export default TaskList;
