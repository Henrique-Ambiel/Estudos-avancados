# 🎮 Persistent Singleton para Unity

## 🕹️ Sobre o Projeto
Este projeto implementa um Singleton Persistente para Unity, garantindo que uma única instância de um objeto permaneça ativa entre as cenas do jogo.

## 🚀 Tecnologias Utilizadas

- 🎮 Engine: Unity (versão utilizada no desenvolvimento)
- 💻 Linguagem: C#
- 🏗️ Padrão de Design: Singleton
  
## ⚙️ Como Funciona
1️⃣ O sistema verifica se já existe uma instância do objeto.

2️⃣ Se não houver, ele tenta carregar um prefab correspondente da pasta Resources.

3️⃣ Caso não encontre, cria um novo objeto na cena e o define como instância única.

4️⃣ O objeto persiste entre as cenas com DontDestroyOnLoad.

5️⃣ Se uma nova instância for criada acidentalmente, ela é destruída para evitar duplicação.

## 🎯 Objetivo
Este projeto foi desenvolvido como parte das atividades da disciplina Estudos Avançados no curso de Jogos Digitais da PUC Campinas 🎓.
